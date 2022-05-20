using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
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

        private string portname;

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

        private readonly object PortUpdateLock = new object();

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

            //if (serialPort.IsOpen && debugPrintEnabled)
            //    dgvLogger.Print("Received " + serialPort.BytesToRead + " bytes", Color.DarkOrange);

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
            Console.WriteLine("OpenCloseButtonHandler");

            if (WaitForSerialPortThread != null && WaitForSerialPortThread.IsAlive)
            {
                WaitForSerialPortThread.Abort();
                UpdatePortStateProperty(SerialPortState.Closed);
                return;
            }

            if (serialPort.IsOpen)
            {
                Console.WriteLine("IsOpen");
                if (this.IsSerialportEventHandlerActive) // ja ir aktiiva eventhandlera funkcija kas lasa ienākošos datus tad portu taisīsim ciet ieksh šīs funkcijas
                {
                    Console.WriteLine("IsSerialportEventHandlerActive");
                    this.CloseMainSerialport = true;
                    UpdatePortStateProperty(SerialPortState.Closing);
                }
                else
                {
                    Console.WriteLine("else");
                    serialPort.Close();
                    UpdatePortStateProperty(SerialPortState.Closed);
                }

                return;
            }

            WaitForSerialPortThread = new Thread(new ThreadStart(WaitForSerialPort));
            WaitForSerialPortThread.Name = "WaitForSerialPortThread";
            WaitForSerialPortThread.Start();

            Console.WriteLine("2");
        }

        public void WaitForSerialPort() // used as thread
        {
            while (true)
            {
                if (!SerialPort.GetPortNames().Contains(serialPort.PortName))
                {
                    UpdatePortStateProperty(SerialPortState.WaitForPortDoesntExists);
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    serialPort.PortName = this.portname;
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

            Console.WriteLine("done");
            //deviceManagementThreadStart = new ThreadStart(deviceManagementThreadMethod);
            //deviceManagementThread = new Thread(deviceManagementThreadStart);
            //deviceManagementThread.Name = "deviceManagementThread";
            //deviceManagementThread.Start();
        }

        ///**
        // * <Overriding windows function to detect usb device add/remove event>
        // * 
        // * @param  none
        // * @return nothing
        // */
        //protected override void WndProc(ref Message m)
        //{
        //    const int WM_DEVICECHANGE = 0x0219;

        //    switch (m.Msg)
        //    {
        //        case WM_DEVICECHANGE:
        //            if ((int)m.WParam == 0x8004)
        //            {
        //                // USB DEVICE REMOVED
        //                dgvLogger.Print("device removed", Color.Violet);
        //            }
        //            else if ((int)m.WParam == 0x8000)
        //            {
        //                // USB DEVICE ATTACHED
        //                dgvLogger.Print("device attached", Color.Violet);
        //            }

        //            const bool startThreadAlways = false;
        //            // Ja seriālais ports pie device change eventa ir atveerts tad pārbaudam kadi devaici ir piesleegti PC
        //            if (serialPort.IsOpen || startThreadAlways || virtualSerialPortAttached)
        //            {
        //                if (deviceManagementThread != null && deviceManagementThread.IsAlive == false)
        //                {
        //                    deviceManagementThreadStart = new ThreadStart(deviceManagementThreadMethod);
        //                    deviceManagementThread = new Thread(deviceManagementThreadStart);
        //                    deviceManagementThread.Name = "deviceManagementThread";
        //                    deviceManagementThread.Start();
        //                }
        //            }
        //            break;
        //    }
        //    base.WndProc(ref m);
        //}

    }
}
