using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SerialTerminal
{
    public class SerialPortPresenter
    {
        enum SerialPortState
        {
            Closed,
            Closing,
            Open,
            WaitForPortDoesntExists,
            WaitForPortAccessDenied,
        }

        private readonly ISerialPortView View;
        private SerialPort serialPort;

        private int SerialPortErrorCounter = 0;
        private int SerialPortPinChangedCounter = 0;

        private bool CloseMainSerialport = false;
        private bool IsSerialportEventHandlerActive = false;

        private Thread WaitForSerialPortThread;
        private Thread SerialDataReceiveThread;
        private Thread USBDeviceManagementThread;

        private string portname;
        private bool WaitForSerialPortFlag;

        private string LineEnding = "";

        public SerialPortPresenter(ISerialPortView view)
        {
            this.View = view;

            this.serialPort = new SerialPort();
            this.serialPort.Close();

            UpdatePortStateProperty(SerialPortState.Closed);

            this.serialPort.ErrorReceived += this.serialPort_ErrorReceived;
            this.serialPort.PinChanged += this.serialPort_PinChanged;
            this.serialPort.DataReceived += serialPort_DataReceived;

            this.View.OnOpenCloseButtonPressed += OpenCloseButtonHandler;

            this.View.OnSerialPortNameChanged += PortNameChanged;
            this.View.OnSerialBaudrateChanged += BaudrateChanged;

            this.View.OnUsbDeviceChange += UsbDeviceChanged;

            this.View.OnLineEndingsChanged += UpdateLineEnding;
            this.View.OnSerialDataTransmit += SerialPortDataTransmit;
            this.View.OnPeriodicDataToggle += TogglePeriodcDataTransmit;
            

        }

        ~SerialPortPresenter()
        {
            this.serialPort.ErrorReceived -= this.serialPort_ErrorReceived;
            this.serialPort.PinChanged -= this.serialPort_PinChanged;
            this.serialPort.DataReceived -= serialPort_DataReceived;

            this.View.OnOpenCloseButtonPressed -= OpenCloseButtonHandler;
        }

        private void BaudrateChanged(string baudrate)
        {
            Console.WriteLine($"baudrate changed {baudrate}");

            int baud;
            if (!int.TryParse(baudrate, out baud))
            {
                return;
            }

            try
            {
                // some custom baudrate values may not be used
                // (for example 30003 on Internal "COM1", meanwhile USB Virtual port works)
                this.serialPort.BaudRate = baud;
            }
            catch
            {
                return;
            }

            if (this.serialPort.IsOpen)
            {
                this.UpdatePortStateProperty(SerialPortState.Open);
            }

        }
        private void PortNameChanged(string serialportname)
        {
            
            if (!SerialPort.GetPortNames().Contains(serialportname))
            {
                // invalid portname
                return;
            }

            if (this.serialPort.PortName == serialportname)
            {
                // port name didnt change
                return;
            }

            this.portname = serialportname;

            if (this.serialPort.IsOpen)
            {
                OpenCloseButtonHandler(null, null); // close current session port
                OpenCloseButtonHandler(null, null); // open with new portname
            }
        }


        private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialPortPinChangedCounter++;
        }

        private void serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPortErrorCounter++;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.IsSerialportEventHandlerActive = true;

            if (SerialDataReceiveThread == null || SerialDataReceiveThread.IsAlive == false)
            {
                SerialDataReceiveThread = new Thread(new ThreadStart(ASyncSerialDataRead));
                SerialDataReceiveThread.Name = "ASyncSerialDataRead";
                SerialDataReceiveThread.Start();
            }

            if (this.CloseMainSerialport)
            {
                serialPort.Close();
                this.UpdatePortStateProperty(SerialPortState.Closed);
                this.CloseMainSerialport = false;
            }

            this.IsSerialportEventHandlerActive = false;
        }

        private void ASyncSerialDataRead()
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    string data = this.serialPort.ReadExisting();
                    //chars = chars.Replace("\n", Environment.NewLine);
                    this.View.AppendSerialData(data);

                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdatePortStateProperty(SerialPortState state)
        {
            string text = "";

            switch (state)
            {
                case SerialPortState.Closed:
                    text = $"Port closed";
                    break;
                case SerialPortState.Closing:
                    text = $"Closing Port...";
                    break;
                case SerialPortState.Open:
                    text = $"{serialPort.PortName} {serialPort.BaudRate}";
                    break;
                case SerialPortState.WaitForPortDoesntExists:
                    text = "Waiting for port, port doesnt exists";
                    break;
                case SerialPortState.WaitForPortAccessDenied:
                    text = "Waiting for port, access denied";
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            this.View.SerialPortStatusDisplay = text;
        }

        private void OpenCloseButtonHandler(object sender, EventArgs e)
        {
            if (WaitForSerialPortThread != null && WaitForSerialPortThread.IsAlive)
            {
                WaitForSerialPortFlag = false;
                UpdatePortStateProperty(SerialPortState.Closed);
                return;
            }

            if (serialPort.IsOpen)
            {
                if (this.IsSerialportEventHandlerActive) // ja ir aktiiva eventhandlera funkcija kas lasa ienākošos datus tad portu taisīsim ciet ieksh šīs funkcijas
                {
                    this.CloseMainSerialport = true;
                    UpdatePortStateProperty(SerialPortState.Closing);
                }
                else
                {
                    serialPort.Close();
                    UpdatePortStateProperty(SerialPortState.Closed);
                }

                return;
            }

            if (this.portname == null)
            { 
                // empty portname (might happen on startup, if no serial devices are arrached)
                return; 
            }

            WaitForSerialPortFlag = true;
            WaitForSerialPortThread = new Thread(new ThreadStart(WaitForSerialPort));
            WaitForSerialPortThread.Name = "WaitForSerialPortThread";
            WaitForSerialPortThread.Start();
        }

        public void WaitForSerialPort() // used as thread
        {
            while (WaitForSerialPortFlag)
            {
                serialPort.PortName = this.portname;

                if (!SerialPort.GetPortNames().Contains(serialPort.PortName))
                {
                    UpdatePortStateProperty(SerialPortState.WaitForPortDoesntExists);
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    serialPort.Open();
                    UpdatePortStateProperty(SerialPortState.Open);
                    break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"exception: {exception.Message}");
                    UpdatePortStateProperty(SerialPortState.WaitForPortAccessDenied);
                    Thread.Sleep(500);
                }
            }
        }


        /// <summary>
        /// Handles USB device add/remove event (WM_DEVICECHANGE)
        /// 
        /// </summary>
        /// <param name="wParam">contains win32 msg wparam 
        /// (https://docs.microsoft.com/en-us/windows/win32/devio/wm-devicechange)</param>
        void UsbDeviceChanged(int wParam)
        {
            Console.WriteLine("UsbDeviceChanged");
            const int DBT_DEVNODES_CHANGED = 0x0007;
            const int DBT_QUERYCHANGECONFIG = 0x0017;
            const int DBT_CONFIGCHANGED = 0x0018;
            const int DBT_CONFIGCHANGECANCELED = 0x0019;
            const int DBT_DEVICEARRIVAL = 0x8000;
            const int DBT_DEVICEQUERYREMOVE = 0x8001;
            const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
            const int DBT_DEVICEREMOVEPENDING = 0x8003;
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
            const int DBT_DEVICETYPESPECIFIC = 0x8005;
            const int DBT_CUSTOMEVENT = 0x8006;
            const int DBT_USERDEFINED = 0xFFFF;

            if (wParam != DBT_DEVICEREMOVECOMPLETE)
            {
                Console.WriteLine("wParam != DBT_DEVICEREMOVECOMPLETE");
                // get the usb devicees only when usb device is removed from PC
                return;
            }

            //if (!serialPort.IsOpen)
            //{
            //    Console.WriteLine("!serialPort.IsOpen");
            //    // no need to check USB devices, if port is closed
            //    return;
            //}

            if (USBDeviceManagementThread?.IsAlive == true)
            {
                Console.WriteLine("USBDeviceManagementThread?.IsAlive == true");
                // previous USBDeviceManagementThread still running, dont start new one
                return;
            }

            USBDeviceManagementThread = new Thread(new ThreadStart(deviceManagementThreadMethod));
            USBDeviceManagementThread.Name = "deviceManagementThread";
            USBDeviceManagementThread.Start();
        }

        private void deviceManagementThreadMethod() // janomaina nosaukums
        {
            Console.WriteLine("deviceManagementThreadMethod");
            bool deviceIsStillAttached = false;

            try
            {
                // using (var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName"))
                // using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                using (var searcher = new ManagementObjectSearcher("SELECT DeviceID FROM WIN32_SerialPort"))
                {
                    var result = searcher.Get();
                    foreach (var Device in result)
                    {
                        string DeviceManagerCOMPortNumber = Device.GetPropertyValue("DeviceID")?.ToString();

                        if (DeviceManagerCOMPortNumber == null)
                        {
                            continue;
                        }

                        Console.WriteLine($"{DeviceManagerCOMPortNumber} \t\t\t $$$$");

                        if (DeviceManagerCOMPortNumber.Equals((serialPort.PortName)))
                        {
                            deviceIsStillAttached = true;
                            return;
                        }
                    }
                }

                using (var searcher = new ManagementObjectSearcher($"SELECT Name FROM Win32_PnPEntity WHERE Name LIKE '%{serialPort.PortName}%'"))
                {
                    var result = searcher.Get();
                    foreach (var Device in result)
                    {
                        object DeviceName = Device.GetPropertyValue("Name");

                        if (DeviceName == null)
                        { 
                            continue;
                        }

                        Console.WriteLine($"---- {DeviceName.ToString()} **** ");

                        if (DeviceName.ToString().Contains((serialPort.PortName)))
                        {
                            deviceIsStillAttached = true;
                            break;
                        }
                    }
                }

                if (deviceIsStillAttached == false)
                {
                    // todo reopen 
                    OpenCloseButtonHandler(null, null);
                    //dgvLogger.Print("COM port Opened but not found, Closing Port", Color.Red);
                }
            }
            catch (ManagementException me)
            {
                //dgvLogger.Print("An error occurred while querying for WMI data: " + me.Message, Color.Black);
            }
        }

        
        private void UpdateLineEnding(string lineEnding)
        {
            this.LineEnding = lineEnding;
        }

        private void SerialPortDataTransmit(string data)
        {
            if (!this.serialPort.IsOpen)
            {
                return;
            }

            string txData = $"{data}{this.LineEnding}";
            this.serialPort.Write(txData);
        }

        private void TogglePeriodcDataTransmit(string data, int interval)
        {
            // todo
        }
        
    }
}
