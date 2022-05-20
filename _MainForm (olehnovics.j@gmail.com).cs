/**
 * SERIAL PORT APPLICATION 
 * @author Janis Olehnovics <janis.olehnovics@mapon.com>
 * v.0.01
 */


using System;
using System.Collections.Generic; // serial port
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions; // get/post
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/*
 * v00.01.01 (07.12.2013): first realease 
 * v0 - major version 
 * .01 - functionality related version number 
 * .01 - bugfix related version number
 */

namespace SerialTerminal
{
    public partial class FormTerminal : Form
    {
        _dataGridViewLogger dgvLogger = new _dataGridViewLogger();
        byte[] serialPortData = new byte[256];

        /*Thread variables*/
        ThreadStart openSerialPortThreadStart = null;
        Thread openPortThread = null;

        ThreadStart SerialDataReceivedThreadStart = null;
        Thread SerialDataReceivedThread = null;

        ThreadStart deviceManagementThreadStart = null;
        Thread deviceManagementThread = null;

    //    ThreadStart fwUpdateThreadStart = null;
        Thread fwUpdateThread = null;
        
        ThreadStart PeriodicSendingThreadStart = null;
        Thread PeriodicSendingThread = null;
               
        bool autoRetry = true;

        bool virtualSerialPortAttached = false;

        char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };


        bool closeMainSerialport = false;
        bool isSerialportEventHandlerActive = false;

        const string terminalRegistryAddress = @"Software\Terminal";

        string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string appDir = System.IO.Directory.GetCurrentDirectory();
        

        public FormTerminal()
        {
            InitializeComponent();
            dgvLogger.setDataGridTarget(dgv_debug);
        }

        private void deviceManagementThreadMethod() // janomaina nosaukums
        {
            dgvLogger.Print("deviceManagementThreadMethod started", Color.DarkOrange);

            // hacks prieksh win8
            // win 8 : "6.2.0.0"
            string OSVersion = Environment.OSVersion.Version.ToString();
            if (false && OSVersion.Contains("6.2."))
            {
                dgvLogger.Print(OSVersion, Color.Red);
                dgvLogger.Print("windows 8 detected, aborting deviceManagementThreadMethod", Color.Red);
                return;
            }

            bool deviceIsStillAttached = false;
            try
            {
              //   using (var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName"))
                // using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
                {
                    dgvLogger.Print("SELECT * FROM WIN32_SerialPort", Color.Orange);
                    foreach (var Device in searcher.Get())
                    {
                        string DeviceManagerCOMPortNumber = Device.GetPropertyValue("DeviceID").ToString(); // kopā ar using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
                        string DeviceName = Device.GetPropertyValue("Name").ToString();// kopā ar using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
                        
                        if (DeviceManagerCOMPortNumber.Equals((serialPort.PortName)))
                        {
                            deviceIsStillAttached = true;
                            dgvLogger.Print("Using : " + DeviceManagerCOMPortNumber + " Port, device : " + DeviceName, Color.Yellow);
                            break;
                        }
                        //   Console.WriteLine(Device.GetPropertyValue("ProviderType")); // ComPortName : ex. "RS232 Serial Port"
                    }
                }

                if (deviceIsStillAttached == false)
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                    {
                        dgvLogger.Print("SELECT * FROM Win32_PnPEntity", Color.Orange);

                        foreach (var Device in searcher.Get())
                        {
                            try
                            {
                                string DeviceName = Device.GetPropertyValue("Name").ToString();// kopā ar using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort
                                if (DeviceName.Contains((serialPort.PortName)))
                                {
                                    deviceIsStillAttached = true;
                                    virtualSerialPortAttached = true;

                                    dgvLogger.Print("Using : " + DeviceName + " Port, device : " + DeviceName, Color.Blue);
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                dgvLogger.Print(ex.Message,Color.Red);
                            }
                        }
                    }
                }

                dgvLogger.Print("COM port : " + deviceIsStillAttached, Color.Blue);

                if (deviceIsStillAttached == false && autoRetry == true)
                {
                    dgvLogger.Print("COM port Opened but not found, Closing Port", Color.Red);
                    openSerialPortMethod();
                }
            }
            catch (ManagementException me)
            {
                dgvLogger.Print("An error occurred while querying for WMI data: " + me.Message, Color.Black);
            }
            dgvLogger.Print("deviceManagementThreadMethod End", Color.DarkOrange);
        }

        /**
         * <Overriding windows function to detect usb device add/remove event>
         * 
         * @param  none
         * @return nothing
         */
        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;

            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    if ((int)m.WParam == 0x8004)
                    {
                        // USB DEVICE REMOVED
                        dgvLogger.Print("device removed", Color.Violet);
                    }
                    else if ((int)m.WParam == 0x8000)
                    {
                        // USB DEVICE ATTACHED
                        dgvLogger.Print("device attached", Color.Violet);
                    }

                    const bool startThreadAlways = false;
                    // Ja seriālais ports pie device change eventa ir atveerts tad pārbaudam kadi devaici ir piesleegti PC
                    if (serialPort.IsOpen || startThreadAlways || virtualSerialPortAttached)
                    {
                        if (deviceManagementThread != null && deviceManagementThread.IsAlive == false)
                        {
                            deviceManagementThreadStart = new ThreadStart(deviceManagementThreadMethod);
                            deviceManagementThread = new Thread(deviceManagementThreadStart);
                            deviceManagementThread.Name = "deviceManagementThread";
                            deviceManagementThread.Start();
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }


        private void comboBox_Port_DropDown(object sender, EventArgs e)
        {
            comboBox_Port.Items.Clear();
            comboBox_Port.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comboBox_Baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _serialPort.changeBaudrate(serialPort, Convert.ToInt32(comboBox_Baudrate.Text));
        }

        private void openSerialPortMethod()
        {
            if (serialPort.IsOpen || (openPortThread != null && openPortThread.IsAlive)) // ja ports atverts tad megjinam aizvert
            {
                if (serialPort.IsOpen)
                {
                    Console.WriteLine("serialPort.IsOpen");
                    if (isSerialportEventHandlerActive) // ja ir aktiiva eventhandlera funkcija kas lasa ienākošos datus tad portu taisīsim ciet ieksh šīs funkcijas
                    {
                        closeMainSerialport = true;
                        _updateControl.setText("Closing Port...", button_openSerialPort, Color.Black);
                    }
                    else
                    {
                        try
                        {
                            serialPort.Close();
                            _updateControl.setText("port closed", button_openSerialPort, Color.Black);
                        }
                        catch (Exception ex)
                        {
                            // ja nav izdevies aizveert portu jo tāds vairs neeksistē (atraujot usb virtuālos portus tā gadās)
                            if (ex.Message.Contains("The device is not connected.") || ex.Message.Contains("The specified port does not exist."))
                            {
                                //openSerialPortMethod();
                            }
                            else
                            {
                                dgvLogger.Print(ex.Message, Color.Yellow);
                            }
                        }
                    }
                }                
                else
                {
                    Console.WriteLine("serialPort Closed");
                    openPortThread.Abort();
                 //   openSerialPortMethod();
                }
            }
            else
            {
                int baudrate;
                if (int.TryParse(comboBox_Baudrate.Text, out baudrate))
                {
                    _serialPort.changeBaudrate(serialPort, baudrate);
                }

                dgvLogger.Print("Port closed, "  + serialPort.PortName + " , trying to open", Color.Black);
                openSerialPortThreadStart = new ThreadStart(openSerialPortThreadMethod);
                openPortThread = new Thread(openSerialPortThreadStart);
                openPortThread.Name = "openPortThread";
                openPortThread.Start();
            }
        }

        public void openSerialPortThreadMethod() // used as thread
        {
            Console.WriteLine("Open Serial Port Thread started");
            while (true)
            {
                if (_serialPort.isSerialPortAvailable(serialPort.PortName))
                {
                    try
                    {
                        serialPort.Open();
                        _updateControl.setText(serialPort.PortName + " " + serialPort.BaudRate, button_openSerialPort, Color.Black);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("waiting for port, access denied");
                        _updateControl.setText("waiting for port", button_openSerialPort, Color.Black);
                    }

                }
                else
                {
                    Console.WriteLine("waiting for port, port doesnt exists");
                    _updateControl.setText("waiting for port, port doesnt exists", button_openSerialPort, Color.Black);
                }
                Thread.Sleep(500);
                // dgvLogger.Print("Open Serial Port Thread is alive", Color.DarkOrange);
            }
            
            deviceManagementThreadStart = new ThreadStart(deviceManagementThreadMethod);
            deviceManagementThread = new Thread(deviceManagementThreadStart);
            deviceManagementThread.Name = "deviceManagementThread";
            deviceManagementThread.Start();

            dgvLogger.Print("Open Serial Port Thread ended", Color.DarkOrange);
            
        }


        private void comboBox_Baudrate_KeyUp(object sender, KeyEventArgs e)
        {
            dgvLogger.Print("ComboBox keyUp event", Color.Black);
            if (e.KeyCode.Equals(Keys.Enter))
            {
                _serialPort.changeBaudrate(serialPort, Convert.ToInt32(comboBox_Baudrate.Text));
                dgvLogger.Print("Enter", Color.Black);
            }
        }
        

        private void comboBox_Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Port.Text != serialPort.PortName) // ja izvelets cits ports
            {
                if (serialPort.IsOpen)
                {
                    try
                    {
                        serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        dgvLogger.Print(ex.Message, Color.IndianRed);
                    }
                }
                this.serialPort.PortName = comboBox_Port.Text;
            }
        }
        
          

        private void button_openSerialPort_Click(object sender, EventArgs e)
        {
            openSerialPortMethod();
        }

        private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            dgvLogger.Print("SerialPort Error Received", Color.Red);
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool debugPrintEnabled = false;
            isSerialportEventHandlerActive = true;

            if (serialPort.IsOpen && debugPrintEnabled)
                dgvLogger.Print("Received " + serialPort.BytesToRead + " bytes", Color.DarkOrange);

            if (SerialDataReceivedThread == null || SerialDataReceivedThread.IsAlive == false)
            {
                SerialDataReceivedThreadStart = new ThreadStart(serialDataReceivedThreadMethod);
                SerialDataReceivedThread = new Thread(SerialDataReceivedThreadStart);
                SerialDataReceivedThread.Name = "SerialDataReceivedThread";
                SerialDataReceivedThread.Start();
            }

            if(closeMainSerialport)
            {
                serialPort.Close();
                _updateControl.setText("Port Closed", button_openSerialPort, Color.Black);
                closeMainSerialport = false;
            }

            isSerialportEventHandlerActive = false;
        }

        private string decode_RF_TemperatureAndHumidity(string data)
        {
            dgvLogger.Print(data.Length.ToString(), Color.PaleVioletRed);
            byte[] bytearray = _lib.convertHexStringToByteArray(data);
            int v = (bytearray[5] << 8 | bytearray[6]);
            float voltage = (float)(bytearray[5] << 8 | bytearray[6]) / 1000;
            int t = (bytearray[7] << 8 | bytearray[8]);
            float temperature = (float)(bytearray[7] << 8 | bytearray[8]) / 100;
            int h = (bytearray[9] << 8 | bytearray[10]);
            float humidity = (float)(bytearray[9] << 8 | bytearray[10]) / 10;

            return string.Format("{0} : Voltage : {1:0.00}  Temperature : {2:0.0}  Humidity : {3:0}", DateTime.Now, voltage, temperature, humidity);
        }

        private void serialDataReceivedThreadMethod()
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    string chars = this.serialPort.ReadExisting();
                    chars = chars.Replace("\n", Environment.NewLine);
                    _updateControl.setText(chars, textbox_ReceiveBox, Color.Black);

                    Thread.Sleep(10);                        
                }
            }
            catch (Exception ex)
            {
                dgvLogger.Print(ex.Message, Color.Red);
            }
      }

        
        private void PeriodicSendingThreadMethod()
        {
            int period;

            dgvLogger.Print("PeriodicSendingThreadMethod started", Color.DarkOrange);
            try
            {
                period = Convert.ToInt16(textBox_PeriodicDataSendingPeriod.Text);
            }
            catch (Exception ex)
            {
                dgvLogger.Print("ERROR : " + ex.Message, Color.Red);
                return;
            }

         
            while (true)
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        string mytext = textBox_PeriodicDataSendingText.Text + Environment.NewLine;
                        
                        serialPort.Write(mytext);
                        _updateControl.setText(mytext, textbox_ReceiveBox, Color.Black);
                    }
                    else
                    {
                        dgvLogger.Print("Port Closed", Color.Orange);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    dgvLogger.Print("ERROR : " + ex.Message, Color.Red);
                    break;
                }

                Thread.Sleep(period); // 5sek timeout's lai norestartēt uiekārtu 
            }
            dgvLogger.Print("PeriodicSendingThreadMethod ended", Color.DarkOrange);
        }
        

        private void serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            dgvLogger.Print("SerialPort PIN Change event", Color.Red);
        }

        private void FormTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLogFile(textbox_ReceiveBox, "< Application Closed >");
            // aizveram portu
            if (openPortThread != null && openPortThread.IsAlive)
                openPortThread.Abort();

            if (SerialDataReceivedThread != null && SerialDataReceivedThread.IsAlive)
                SerialDataReceivedThread.Abort();
            
            Microsoft.Win32.RegistryKey key;

            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(terminalRegistryAddress);
            key.SetValue("COM Port", this.serialPort.PortName);
            key.SetValue("Baudrate", this.serialPort.BaudRate);

            // ja ir vismaz kaut viens chars fw adresē ievadīts tad pieglabājam reģistros
            if (textBox_fwAddress.Text.Length > 0)
                key.SetValue("FW Address", this.textBox_fwAddress.Text);

            key.SetValue("serialPortFwUpdate", this.serialPortFwUpdate.PortName);

            key.SetValue("textbox_shortcut_1", textbox_shortcut_1.Text);
            key.SetValue("textbox_shortcut_2", textbox_shortcut_2.Text);
            key.SetValue("textbox_shortcut_3", textbox_shortcut_3.Text);
            key.SetValue("textbox_shortcut_4", textbox_shortcut_4.Text);
            key.SetValue("textbox_shortcut_5", textbox_shortcut_5.Text);
            key.SetValue("textbox_shortcut_6", textbox_shortcut_6.Text);
            key.SetValue("textbox_shortcut_7", textbox_shortcut_7.Text);
            key.SetValue("textbox_shortcut_8", textbox_shortcut_8.Text);
            key.SetValue("textbox_shortcut_9", textbox_shortcut_9.Text);
            key.SetValue("textbox_shortcut_10", textbox_shortcut_10.Text);
            key.SetValue("textbox_shortcut_11", textbox_shortcut_11.Text);

            key.SetValue("textBox_PeriodicDataSendingText", textBox_PeriodicDataSendingText.Text);
            key.SetValue("textBox_PeriodicDataSendingPeriod", textBox_PeriodicDataSendingPeriod.Text);
            key.Close();
        }

        private void textbox_SendBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                dgvLogger.Print("Port Error or closed", Color.IndianRed);
                return; 
            }

            char[] chr = new char[] { e.KeyChar };

            try
            {
                serialPort.Write(chr, 0, 1);

                if (e.KeyChar == 13)
                {
                    _updateControl.setText(Environment.NewLine, textbox_ReceiveBox, Color.Black);
                    textbox_SendBox.Clear();
                    e.Handled = true;
                }
                else if (e.KeyChar >= 28)
                {
                    _updateControl.setText(e.KeyChar.ToString(), textbox_ReceiveBox, Color.Black);
                }

            }
            catch (Exception ex)
            {
                _updateControl.setText(ex.Message + Environment.NewLine, textbox_ReceiveBox, Color.Black);
                dgvLogger.Print(ex.Message, Color.IndianRed);
            }
        }



        
        private void FormTerminal_Load(object sender, EventArgs e)
        {
            Array.Clear(serialPortData, 0, serialPortData.Length);
                        
            try
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(terminalRegistryAddress);

                foreach (string SubKeyName in key.GetValueNames())
                {
                    switch (SubKeyName)
                    {
                        case "COM Port":
                            serialPort.PortName = key.GetValue(SubKeyName).ToString();
                            comboBox_Port.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "Baudrate":
                            serialPort.BaudRate = (int)key.GetValue(SubKeyName);
                            comboBox_Baudrate.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "FW Address":
                            textBox_fwAddress.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "serialPortFwUpdate":
                            serialPortFwUpdate.PortName = key.GetValue(SubKeyName).ToString();
                            comboBox_PortFwUpdate.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_1":
                            textbox_shortcut_1.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_2":
                            textbox_shortcut_2.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_3":
                            textbox_shortcut_3.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_4":
                            textbox_shortcut_4.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_5":
                            textbox_shortcut_5.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_6":
                            textbox_shortcut_6.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_7":
                            textbox_shortcut_7.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_8":
                            textbox_shortcut_8.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_9":
                            textbox_shortcut_9.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_10":
                            textbox_shortcut_10.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textbox_shortcut_11":
                            textbox_shortcut_11.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textBox_PeriodicDataSendingText":
                            textBox_PeriodicDataSendingText.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textBox_PeriodicDataSendingPeriod":
                            textBox_PeriodicDataSendingPeriod.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textBox_SourceFileLocation":
                            //textBox_SourceFileLocation.Text = key.GetValue(SubKeyName).ToString();
                            break;
                        case "textBox_HIDBootloaderLocation":
                            //textBox_HIDBootloaderLocation.Text = key.GetValue(SubKeyName).ToString();
                            break;                            
                        default: break;
                    }
                }
               // openSerialPortMethod();
            }
            catch { }

            try
            {
                dgvLogger.Print(Environment.CurrentDirectory, Color.Orange);
                dgvLogger.Print(Environment.OSVersion.ToString(), Color.Orange);
                
                String str = Environment.OSVersion.Version.ToString();
                String OsName = "";
                if (str.Contains("6.2"))
                    OsName = "Windows 8.1";
                else if (str.Contains("6.1.7"))
                    OsName = "Windows 7";
                else if (str.Contains("5.1") || str.Contains("5.2"))
                    OsName = "Windows XP";
                else
                    OsName = "unknown windows version";

                dgvLogger.Print(OsName, Color.Orange);

            }
            catch (Exception ex)
            {
                dgvLogger.Print(ex.Message,Color.Red);
            }
        }
        

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            serialPort.DtrEnable = !serialPort.DtrEnable;
            serialPort.RtsEnable = !serialPort.RtsEnable;
            
            if (serialPort.DtrEnable)
            {
                // ja ieslēgts tad piedāvājam izslēgt
                tsbutton_dtrButton.Text = "Turn Off DTR";
                this.tsbutton_dtrButton.Image = global::TerminalNew.Properties.Resources.checkGreen;
            }
            else 
            {
                tsbutton_dtrButton.Text = "Turn On DTR";
                this.tsbutton_dtrButton.Image = global::TerminalNew.Properties.Resources.checkRed;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            // start thread fw update thread
            // ja threads veel neeksistē (būs pirmā palaišana), vai arī ja threads nav aktīvs tad palaižam
            if (fwUpdateThread == null || fwUpdateThread.IsAlive == false)
            {
                fwUpdateThread = new Thread(fwUpdateThreadMethod);
                fwUpdateThread.Name = "fwUpdateThread";
                fwUpdateThread.Start("firmware");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // start thread fw update thread
            // ja threads veel neeksistē (būs pirmā palaišana), vai arī ja threads nav aktīvs tad palaižam
            if (fwUpdateThread == null || fwUpdateThread.IsAlive == false)
            {
                fwUpdateThread = new Thread(fwUpdateThreadMethod);
                fwUpdateThread.Name = "fwUpdateThread";
                fwUpdateThread.Start("settings");
            }
        }


        
        private void fwUpdateThreadMethod(object hexType)
        {
            serialPortFwUpdate.Encoding = System.Text.Encoding.GetEncoding(1252);
            string hexFileType = (string)hexType;
            string hexFilePath = "";

            dgvLogger.Print(hexFileType, Color.Orange);

            Stopwatch stopWatch = new Stopwatch();
            //   int timeoutInMiliseconds = 1500; // 1500ms = 1.5sec
            if (hexFileType.Equals("firmware"))
            {
                hexFilePath = textBox_fwAddress.Text;
                dgvLogger.Print("fw", Color.Blue);
            }
            else if (hexFileType.Equals("settings"))
            {
                hexFilePath = textBox_settingsAddress.Text;
                dgvLogger.Print("set", Color.Blue);
            }
            else
            {
                dgvLogger.Print("ERROR", Color.Red);
                return;
            }

            if (File.Exists(hexFilePath))
            {
                int hexFileLineCount = TotalLines(hexFilePath);
                int currentHexLine = 1;

                dgvLogger.Print("Firmware File Exists, Line count = " + hexFileLineCount, Color.Blue);

                try
                {
                    // ja abi serialportobjekti izmanto vienu un to pashu portu, un tas jau ir atvērts tad aizveram ciet serialPort izmantoto portu
                    if (serialPortFwUpdate.PortName == serialPort.PortName && serialPort.IsOpen == true)
                    {
                        try
                        {
                            serialPort.Close();
                            _updateControl.setText("port closed", button_openSerialPort, Color.Black);
                        }
                        catch (Exception ex)
                        {
                            dgvLogger.Print("ex : " + ex.Message, Color.IndianRed);
                            dgvLogger.Print("serialPort.IsOpen Status : " + serialPort.IsOpen, Color.IndianRed);

                            // ja nav izdevies aizveert portu jo tāds vairs neeksistē (atraujot usb virtuālos portus tā gadās)
                            if (ex.Message.Contains("The device is not connected."))
                            {
                                dgvLogger.Print("reinvoking open port button event", Color.Blue);
                            }
                        }
                    }

                    if (serialPortFwUpdate.IsOpen == false)
                        serialPortFwUpdate.Open();

                    dgvLogger.Print("Opened Serial Port : " + serialPortFwUpdate.PortName + " @ baudrate : " + serialPortFwUpdate.BaudRate.ToString(), Color.Blue);
                }
                catch (Exception ex)
                {
                    dgvLogger.Print("ex : " + ex.Message, Color.Red);
                    return;
                }

                

                // Open the selected file to read.
                System.IO.Stream hexFileStream = File.OpenRead(hexFilePath);

                using (System.IO.StreamReader hexFileReader = new System.IO.StreamReader(hexFileStream))
                {
                    // kamēr nav pienākušas faila beigas, tikmēr lasam faila saturu
                    bool updateFinished = false;
                    bool updateStarted = false;

                    string charD4 = char.ConvertFromUtf32(0xD4);
                    string charC7 = char.ConvertFromUtf32(0xC7);
                    string charE1 = char.ConvertFromUtf32(0xE1);
                    string charAC = char.ConvertFromUtf32(0xAC);

                    if (hexFileType.Equals("settings"))
                    {
                        serialPortFwUpdate.Write("downl\r");
                        dgvLogger.Print("sending : 'downl'", Color.Blue);
                    }

                    const int timeConstant = 3000; // ms
                    const int threadSleepConstant = 5; // ms
                    
                    dgvLogger.Print(timeConstant.ToString() + " ms",Color.IndianRed);

                    while (hexFileReader.EndOfStream == false && updateFinished == false)
                    {
                        bool timeoutOccured = true;

                        int loopEndConstant = timeConstant / threadSleepConstant;
                        for (int i = 0; i < loopEndConstant; i++)
                        {
                            if (serialPortFwUpdate.BytesToRead > 0)
                            {
                                //   dgvLogger.Print("Received " + serialPortFwUpdate.BytesToRead + "byte(s)", Color.DarkOrange);
                                if (hexFileType.Equals("settings"))
                                {
                                    Thread.Sleep(20);
                                }

                                // nolasam to kas atnacis un paarabudam
                                string serialPortResult = serialPortFwUpdate.ReadExisting();

                                //   _updateControl.setText("Rx : " + serialPortResult + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);

                                // dgvLogger.Print("Rx : " + serialPortResult, Color.Blue);

                                if (serialPortResult.Length == 1)
                                {
                                    byte[] ba = System.Text.Encoding.GetEncoding(1252).GetBytes(serialPortResult);
                                   // dgvLogger.Print("Rx : " + string.Format("0x{0:X2}", ba[0]), Color.Blue);
                                }

                                if (serialPortResult.Equals("A") || serialPortResult.Equals(charAC) || serialPortResult.Equals("ACK")) // A - picFlasher 10xxxx, 0xAC-settings 10xxxx, 'ACK'- settings 15xxxx-18xxxx
                                {
                                    //    fwUpdateProgresBar.Value++;
                                    string hexLine = hexFileReader.ReadLine();

                                    serialPortFwUpdate.Write(hexLine + "\r");

                                   // _updateControl.setText("Tx : " + currentHexLine++ + "/" + hexFileLineCount + " - " + hexLine + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);
                                    _updateControl.setText("Tx : " + currentHexLine++ + "/" + hexFileLineCount + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);

                            //        Thread.Sleep(100); // KAASIM!!
                                    timeoutOccured = false;
                                    break;
                                }
                                if (serialPortResult.Equals("W") || serialPortResult.Equals(charD4))
                                {
                                    // gboxis norestartēts un iebootojies. sūutam pirmo pakas

                                    stopWatch.Start();
                                    updateStarted = true;

                                    // noresetojam filereader streamu, lai lasa atkal pirmo rindu
                                    hexFileReader.DiscardBufferedData();
                                    hexFileReader.BaseStream.Seek(0, SeekOrigin.Begin);
                                    hexFileReader.BaseStream.Position = 0;

                                    string hexLine = hexFileReader.ReadLine();
                                    
                                    if (hexFileType.Equals("firmware"))
                                        serialPortFwUpdate.Write("S" + hexLine + "\r");
                                    else if (hexFileType.Equals("settings"))
                                        serialPortFwUpdate.Write(hexLine + "\r");
                                    
                                    _updateControl.setText("Tx : " + currentHexLine++ + "/" + hexFileLineCount + " - " + hexLine + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);
                                    // nolasam vienu rindu un apstrādājam to
                                    //      dgvLogger.Print("S" + hexLine, Color.Red);  

                                    timeoutOccured = false;
                                    break;
                                }
                                else if (serialPortResult.Equals("E") || serialPortResult.Equals(charE1))
                                {
                                    _updateControl.setText("Rx : ERROR, NACK received" + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);
                                }
                                else if (serialPortResult.Equals("EE"))
                                {
                                    _updateControl.setText("Rx : ERROR, Timeout Occured" + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);
                                }
                                else if (serialPortResult.Equals("AQ") || serialPortResult.Equals(charC7))
                                {
                                    dgvLogger.Print("Firmware Updated", Color.Green);
                                    _updateControl.setText("Firmware Updated" + Environment.NewLine, textBox_fwUpdateDataExchange, Color.Black);

                                    updateFinished = true;
                                    timeoutOccured = false;
                                    break;
                                }

                                //   Thread.Sleep(10); //
                                //   timeoutOccured = false;
                                //   break;
                            }

                            //      dgvLogger.Print("for thread is alive", Color.Blue);
                            if (updateStarted || hexFileType.Equals("settings"))
                                Thread.Sleep(threadSleepConstant); // 100ms timeout's lai saņmetu atbildi no gboxa
                            else
                                Thread.Sleep(250); // 5sek timeout's lai norestartēt uiekārtu 
                        }

                        if (timeoutOccured)
                        {
                            dgvLogger.Print("ERROR, Timeout Occured", Color.Red);

                            serialPortFwUpdate.Close();
                            if (serialPortFwUpdate.PortName == serialPort.PortName)
                            {
                                dgvLogger.Print("Closing Port : " + serialPortFwUpdate.PortName, Color.Blue);
                                this.Invoke(new EventHandler(button_openSerialPort_Click)); // imitējam open Port Button Click
                            }

                            return;
                        }
                    }

                    if (hexFileType.Equals("settings"))
                    {
                        serialPortFwUpdate.Write(":00000001FF\r");
                        dgvLogger.Print("Sending ':00000001FF'", Color.Blue);
                    }

                }
                hexFileStream.Close();
            }
            else
            {
                dgvLogger.Print("Firmware File DOES NOT Exists", Color.Red);
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            serialPortFwUpdate.Close();
            if (serialPortFwUpdate.PortName == serialPort.PortName)
            {
                dgvLogger.Print("Closing Port : " + serialPortFwUpdate.PortName, Color.Blue);
                this.Invoke(new EventHandler(button_openSerialPort_Click)); // imitējam open Port Button Click
            }
            dgvLogger.Print(elapsedTime, Color.DarkOrange);
        }

        private string openFileDialog()
        {
            OpenFileDialog openFwFileDialog = new OpenFileDialog();
            openFwFileDialog.Filter = "Text Files (.txt)|*.txt";
            openFwFileDialog.FilterIndex = 1;
            openFwFileDialog.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFwFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                return openFwFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        private void button_openHEXFile_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFwFileDialog= new OpenFileDialog();

            // Set filter options and filter index.
            openFwFileDialog.Filter = "Hex Files (.hex)|*.hex";
            openFwFileDialog.FilterIndex = 1;

            openFwFileDialog.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFwFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                textBox_fwAddress.Text = openFwFileDialog.FileName;
            }
            else
            {
                if(textBox_fwAddress.Text.Length == 0)
                    textBox_fwAddress.Text = "No File Selected";
            }
        }

        private void comboBox_PortFwUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_PortFwUpdate.Text != serialPortFwUpdate.PortName) // ja izvelets cits ports
            {
                try
                {
                    this.serialPortFwUpdate.PortName = comboBox_PortFwUpdate.Text;
                }
                catch (Exception ex)
                {
                    dgvLogger.Print(ex.Message, Color.Red);
                }
            }
        }

        private void comboBox_PortFwUpdate_DropDown(object sender, EventArgs e)
        {
            comboBox_PortFwUpdate.Items.Clear();
            comboBox_PortFwUpdate.Items.AddRange(SerialPort.GetPortNames());
        }

        private void toolStrip_autoRetry_Click(object sender, EventArgs e)
        {
            autoRetry = !autoRetry;
            tsbutton_autoRetry.Text = "autoRetry = " + autoRetry;

            if (autoRetry)
                this.tsbutton_autoRetry.Image = global::TerminalNew.Properties.Resources.RefreshGreen;
            else
                this.tsbutton_autoRetry.Image = global::TerminalNew.Properties.Resources.RefreshRed;

            dgvLogger.Print("autoRetry : " + autoRetry, Color.IndianRed);
        }


        
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = !splitContainer2.Panel1Collapsed;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
        }
        
        private void buttonProcessing(TextBox textbox)
        {
            dgvLogger.Print("buttonProcessing func", Color.Black);
            SerialPort sp = serialPort;
            object obj = textbox_ReceiveBox;

            if (textbox.TextLength != 0) // ja texbox'i ir kaut kas ierakst'tis tad sūtam datus uz serialo portu
            {
                    if (sp.IsOpen)
                    {
                        string newline = Environment.NewLine;

                        if(radioButton_RN.Checked)
                                newline = "\r\n";
                        else if(radioButton_N.Checked)
                                newline = "\n";
                        else if(radioButton_R.Checked)
                                newline = "\r";
                        else
                            dgvLogger.Print("incorrect radio button value");

                        try
                        {
                            sp.Write(textbox.Text + Environment.NewLine);
                            _updateControl.setText(textbox.Text + Environment.NewLine, obj, Color.Black);
                        }
                        catch (Exception ex)
                        {
                            dgvLogger.Print("buttonProcessing ERROR : " + ex.Message, Color.Red);
                        }
                    }
                    else
                    {
                        dgvLogger.Print("Port Closed", Color.Orange);
                    }
            }
        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dgvLogger.dgvScroolToCarret = !dgvLogger.dgvScroolToCarret;

            tsbutton_ScroolToCarret.ToolTipText = dgvLogger.dgvScroolToCarret.ToString();

            if (dgvLogger.dgvScroolToCarret)
                tsbutton_ScroolToCarret.Image = global::TerminalNew.Properties.Resources.downGreen;
            else
                tsbutton_ScroolToCarret.Image = global::TerminalNew.Properties.Resources.downRed;

            dgvLogger.Print("ScroolToCarret : " + dgvLogger.dgvScroolToCarret, Color.IndianRed);
        }
                
        private void button3_Click_1(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_7);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_9);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_11);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_2);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_4);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_6);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            buttonProcessing(textbox_shortcut_10);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (PeriodicSendingThread == null || PeriodicSendingThread.IsAlive == false)
            {
                dgvLogger.Print("button 9 clicked", Color.DarkOrange);
                PeriodicSendingThreadStart = new ThreadStart(PeriodicSendingThreadMethod);
                PeriodicSendingThread = new Thread(PeriodicSendingThreadStart);
                PeriodicSendingThread.Name = "PeriodicDataSending";
                PeriodicSendingThread.Start();
            }
            else if (PeriodicSendingThread.IsAlive)
            {
                dgvLogger.Print("PeriodicSendingThread aborted", Color.Red);
                PeriodicSendingThread.Abort();
            }
        }
        

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            dgvLogger.Clear();
        }

        
        private void button21_Click(object sender, EventArgs e)
        {
                if (serialPortFwUpdate.IsOpen)
                {
                    try
                    {
                        serialPortFwUpdate.Close();
                        dgvLogger.Print("port opened, closing.. " + serialPortFwUpdate.PortName, Color.Green);
                    }
                    catch (Exception ex)
                    {
                        dgvLogger.Print(ex.Message, Color.IndianRed);
                    }
                }

                try
                {
                    serialPortFwUpdate.Open();
                    dgvLogger.Print("port opened, " + serialPortFwUpdate.PortName, Color.Green);
                }
                catch (Exception ex)
                {
                    dgvLogger.Print(ex.Message, Color.IndianRed);
                }

        }

        private void button23_Click(object sender, EventArgs e)
        {

            // Create an instance of the open file dialog box.
            OpenFileDialog openFwFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFwFileDialog.Filter = "Hex Files (.hex)|*.hex";
            openFwFileDialog.FilterIndex = 1;

            openFwFileDialog.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFwFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                textBox_settingsAddress.Text = openFwFileDialog.FileName;
            }
            else
            {
                if (textBox_settingsAddress.Text.Length == 0)
                    textBox_settingsAddress.Text = "No File Selected";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

                Stopwatch watch = new Stopwatch();
                string test = int.MaxValue.ToString();

                watch.Reset();
                watch.Start();
                for (int i = 0; i < 1; i++)
                {
                    string path = @"C:\Users\olga\Dropbox\Teltonika #1";
                    string search = "log-file";

                    foreach(string s in Directory.GetFiles(path, "*" + search + "*", SearchOption.AllDirectories))
                    {
                        dgvLogger.Print(s,Color.Violet);
                        float length = new System.IO.FileInfo(s).Length / (float)1048576;
                        string dataSize = length.ToString();
                        dgvLogger.Print(dataSize.Substring(0,4)+ " MB", Color.Red);
                        File.Delete(s);
                    }
                }
                watch.Stop();

                dgvLogger.Print(watch.ElapsedTicks.ToString(),Color.Blue);
            
                Console.WriteLine("IsDigitsOnly: " + watch.ElapsedTicks);
        }

        void SaveLogFile(TextBox txtbox, string textFileHeader)
        {
            if (txtbox.TextLength > 0)
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\log"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\log");
                }

                string filename = Directory.GetCurrentDirectory() + "\\log\\" + DateTime.Now.ToString("MM.dd HH-mm-ss") + ".txt";
                using (StreamWriter logfile = File.CreateText(filename)) // replace veco textu
                {
                    logfile.Write(textFileHeader + Environment.NewLine);                    
                    logfile.Write(txtbox.Text);
                }

                dgvLogger.Print("logfile saved : " + filename, Color.Orange);
            }
        }
        

        private bool IsDigitsOnly(string testString)
        {
            foreach (char c in testString)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
       
        private void moveTeltonikaToUser(string phpSessionID,int id)
        {
            string url = "http://mapon.com/partner/gbox_list";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Cookie", phpSessionID);

            var postData = "";

            postData += "c_box=";
            postData += id.ToString();
            postData += "&c_tp=UZSTADITAJIEM&c_adj=67&c_company=&act2=box_move&model_filter=";
            postData = "c_box=1200035&c_tp=UZSTADITAJIEM&c_adj=67&c_company=&act2=box_move&model_filter=QUECLINK";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            _updateControl.setText(responseString, textbox_ReceiveBox, Color.Black);

            dgvLogger.Print(postData, Color.Orange);
        }


        private string addDevice_maponAPI(string ID , string phone = "00000000")
        {
            string url = "http://www.mapon.com/api/own_services/?key=H2khp1KyAGLccFexDgIQWRakt0dKTG9f&action=RegNewDevice&params[model]=G-BOX2&params[id]=" + ID + "&params[phone]=" + phone;
            dgvLogger.Print(url, Color.Green);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dgvLogger.Print(responseString, Color.Green);
            return responseString;

        }

        private string maponHTMLPostMsg(string url, string postData, string phpSessionID)
        {
            dgvLogger.Print(url, Color.Green);
            dgvLogger.Print(postData, Color.Green);
            dgvLogger.Print(phpSessionID, Color.Green);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Cookie", "PHPSESSID=" + phpSessionID);

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dgvLogger.Print(responseString, Color.Green);
            return responseString;
        }

        private string maponHTMLGetMsg(string url, string sessionID)
        {
            dgvLogger.Print(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Cookie", "PHPSESSID=" + sessionID);
//            dgvLogger.Print(request.Headers.Get("Cookie"));

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString == "r")
            {
                _updateControl.setText("LOGIN REQUIRED", dgv_debug, Color.Red);
            }
            else
                _updateControl.setText(responseString, dgv_debug, Color.DarkGreen);

            return responseString;
        }

/*      TESTAM 
        private string getNextTeltonikaID(string sessionID)
        {
            string url = "http://mapon.com/partner/ajax.php?module=gbox_new&sub=next_id&model=TELTONIKA";
            string HTMLGetResult = maponHTMLGetMsg(url, sessionID);

            string[] splittedResponseString = Regex.Split(HTMLGetResult, @"\D+");

            foreach (string s in splittedResponseString)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    _updateControl.setText(s, dgw_debug, Color.Blue);
                    return s;
                }
            }
            return null;
        }
*/

        private string getNextTeltonikaID(string sessionID)
        {
            string url = "http://mapon.com/partner/ajax.php?module=gbox_new&sub=next_id&model=TELTONIKA";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Cookie", "PHPSESSID=" + sessionID);
            //  request.Headers.Add("Cookie", sessionID);
            dgvLogger.Print(request.Headers.Get("Cookie"));

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString == "r")
            {
                _updateControl.setText("LOGIN REQUIRED", dgv_debug, Color.Red);
            }
            else
                _updateControl.setText(responseString, dgv_debug, Color.DarkGreen);

            string[] splittedResponseString = Regex.Split(responseString, @"\D+");
            foreach (string s in splittedResponseString)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    _updateControl.setText(s, dgv_debug, Color.Blue);
                    return s;
                }
            }
            return null;
        }

        private bool checkBaudrate(SerialPort sp, int baudrate , int timoutInMilis)
        {
            sp.ReadTimeout = timoutInMilis / 10 ;
            sp.BaudRate = baudrate;
            sp.Write("AT\r\n");

            for (int i = 0; i < 10; i++)
            {
                dgvLogger.Print("i : " + i, Color.OrangeRed);
                while (sp.BytesToRead > 1)
                {
                    try
                    {
                        string serialPortResult = sp.ReadLine();
                        dgvLogger.Print(serialPortResult);
                        if (serialPortResult.Contains("OK"))
                        {
                            dgvLogger.Print("baudrate : " + baudrate);
                            return true;
                        }
                    }
                    catch (TimeoutException)
                    {
                        dgvLogger.Print("TimeoutException", Color.OrangeRed);
                        break;
                    }   
                }
            }

            dgvLogger.Print("baudrate : " + baudrate + ", FALSE", Color.Red);
            return false;
        }

        private void getTeltonikaParametersFromFile(int parameterCount)
        {
            string[,] array = new string[1, parameterCount];
            string fileLocation = "";
            // Create an instance of the open file dialog box.
            OpenFileDialog openFwFileDialog= new OpenFileDialog();

            // Set filter options and filter index.
            openFwFileDialog.Filter = "Text Files (.txt)|*.txt";
            openFwFileDialog.FilterIndex = 1;

            openFwFileDialog.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFwFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                fileLocation = openFwFileDialog.FileName;
            }
            else
            {
                fileLocation = "No File Selected";
                return;
            }

            System.IO.Stream txtFileStream = File.OpenRead(fileLocation);
            using (System.IO.StreamReader txtFileReader = new System.IO.StreamReader(txtFileStream))
            {
                while (txtFileReader.EndOfStream == false)
                {
                    string txtLine = txtFileReader.ReadLine();
                    char[] splitChars = new char[] { '\t', ' ' };

                    string[] parameters = txtLine.Split(splitChars);
                    createNewTeltonika(parameters);
                }
            }
        }

        private bool checkNumberForValidity()
        {
            return true;
        }

        private bool createNewTeltonika(string[] parameters)
        {
            if (parameters.Length != 3)
            {
                dgvLogger.Print("wrong parameter count", Color.Red);
                return false;
            }

            string url = "http://mapon.com/partner/ajax.php?module=gbox_new&sub=create";

            string phoneNumber = parameters[2];
            string SerialNumber = parameters[0];
            string IMEI = parameters[1];
            
            string model = "TELTONIKA";

            int IMEILenght = 15;
            int SerialNumberLenght = 8;
            int phoneNumberLenght = 8;

            if (model.Equals("LEVEL"))
            {
                SerialNumberLenght = 7;
                phoneNumberLenght = 13;            }

            bool errorFlag = false;

            if (IMEI.Length != IMEILenght)
            {
                _updateControl.setText("wrong IMEI Lenght", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (SerialNumber.Length != SerialNumberLenght)
            {
                _updateControl.setText("wrong SerialNumber Lenght", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (false && phoneNumber.Length != phoneNumberLenght)
            {
                _updateControl.setText("wrong phoneNumber Lenght", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (IsDigitsOnly(IMEI) == false)
            {
                _updateControl.setText("wrong chars in IMEI", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (IsDigitsOnly(SerialNumber) == false)
            {
                _updateControl.setText("wrong chars in SerialNumber", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (IsDigitsOnly(phoneNumber.Replace('+','0')) == false)
            {
                _updateControl.setText("wrong chars in phoneNumber", dgv_debug, Color.Red);
                errorFlag = true;
            }

            if (errorFlag == true)
            {
                _updateControl.setText("Operation Canceled", dgv_debug, Color.Red);
                return false;
            }
                /*
                 * model=TELTONIKA&model_ver=FM1100&box_id=1000367&device_id=&port2send=&sn=04409895&hardware=&firmware=&firmware2=&identification=&software_rw=&imei=356307047523971&sim=28663689&sim_owner=mapon&imsi=&action=add_box
                 */

                string postData = "";
                //string model = "LEVEL";

                if (model.Equals("TELTONIKA"))
                {
                    postData += "model=TELTONIKA";
                    
                    postData += "&imei=";
                    postData += IMEI; // sērijas nummurs sheit!!! kā strings

                    postData += "&sn=";
                    postData += SerialNumber; // sērijas nummurs sheit!!! kā strings

                    postData += "&sim=";
                    postData += phoneNumber; // sērijas nummurs sheit!!! kā strings

                    postData += "&model_ver=";
                    postData += comboBox_TeltonikaModelVersion.Text; // sērijas nummurs sheit!!! kā strings

                }
                else if (model.Equals("LEVEL"))
                {                    
                    postData += "model=LEVEL&model_ver=GC75";
                    postData += "box_id=&client_device=on&client_device_client=3848&client_device_client_id=8349&device_id=&port2send=";

                    postData += "&sn=" + "0"; // add '0' prefix 
                    postData += SerialNumber; // sērijas nummurs sheit!!! kā strings

                    postData += "&hardware=&firmware=&firmware2=&identification=&software_rw=&imei=";

                    postData += "&sim=";
                    postData += phoneNumber.Replace("+", "%2B"); 

                    postData += "&imsi=&customFields%5Bciphered%5D=1&customFields%5Benc_key%5D=2040EE97ACE11643AF29E2FA2DAC20D2&customFields%5Bpacked_records%5D=1&action=add_box"; 
                    
                }

                DialogResult dialogRes;
                dialogRes = MessageBox.Show(postData, "OK?", MessageBoxButtons.OKCancel);
                if (dialogRes == DialogResult.OK)
                {
                    _updateControl.setText("OK", dgv_debug, Color.Blue);
                }
                else if (dialogRes == DialogResult.Cancel)
                {
                    _updateControl.setText("CANCEL", dgv_debug, Color.Blue);
                    return false;
                }

                var data = Encoding.ASCII.GetBytes(postData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
             //   request.Headers.Add("Cookie", "PHPSESSID=" + textbox_phpSessionID.Text);
                request.Headers.Add("Cookie", "PHPSESSID=" + "123");

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                /*
          OK     * <xml><status>OK</status><action>document.location.href='/partner/gbox_list?badd';</action></xml>
          ERROR  * <xml><error><field>imei</field><msg>IMEI pastāv!</msg></error><error><field>sn</field><msg>Sērijas numurs jau eksistē</msg></error></xml>
                 */
                _updateControl.setText(responseString, dgv_debug, Color.Orange);

                if (responseString.Contains("<xml><status>OK</status>"))
                {
                    //saveTextFile(responseString + "," + IMEI + "," + SerialNumber + "," + phoneNumber);
                    _updateControl.setText("Box Created", dgv_debug, Color.Green);
                    return true;
                }
                else if (responseString.Contains("<xml><error>"))
                {
                    MessageBox.Show("ERROR", responseString);
                }

                return false;
        }
            
        private void tsbutton_clearDebugWindow_Click(object sender, EventArgs e)
        {
            dgvLogger.Clear();
        }

        private void comboBox_BaudrateFwUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_BaudrateFwUpdate.Text != serialPortFwUpdate.BaudRate.ToString()) // ja izvelets cits ports
            {
                dgvLogger.Print("baudrate changed");
                try 
                {
                    this.serialPortFwUpdate.BaudRate = Convert.ToInt32(comboBox_BaudrateFwUpdate.Text);
                }
                catch(Exception ex)
                {
                    dgvLogger.Print(ex.Message);
                }
            }
        }

        private void tabContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvLogger.Print("tab changed");
            if (tabContainer.SelectedTab == tabContainer.TabPages["tab_gbox4FwUpd"])
            {
                comboBox_BaudrateFwUpdate.Text = "115200";
                serialPortFwUpdate.BaudRate = 115200;
                
                if (label_IMEI.Text.Equals("IMEI Not Set")) // ja texts : IMEI Not Set 
                {
                    button_createNewQueclink.Enabled = false;
                    button_createNewTeltonika.Enabled = false;
                }
            }
        }
        

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }


       
        // 
        /// <summary>
        /// Gens the CRC16.
        /// CRC-1021 = X(16)+x(12)+x(5)+1
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="nByte">The n byte.</param>
        /// <returns>System.Byte[][].</returns>
        public ushort GenCrc16(byte[] c, int nByte)
        {
            ushort Polynominal = 0x1021;
            ushort InitValue = 0x0;

            ushort i, j, index = 0;
            ushort CRC = InitValue;
            ushort Remainder, tmp, short_c;
            for (i = 0; i < nByte; i++)
            {
                short_c = (ushort)(0x00ff & (ushort)c[index]);
                tmp = (ushort)((CRC >> 8) ^ short_c);
                Remainder = (ushort)(tmp << 8);
                for (j = 0; j < 8; j++)
                {

                    if ((Remainder & 0x8000) != 0)
                    {
                        Remainder = (ushort)((Remainder << 1) ^ Polynominal);
                    }
                    else
                    {
                        Remainder = (ushort)(Remainder << 1);
                    }
                }
                CRC = (ushort)((CRC << 8) ^ Remainder);
                index++;
            }
            return CRC;
        }


        private ushort crc_xmodem_update(ushort crc, byte data)
        {
            int i;
            crc = (ushort)(crc ^ (data << 8));

            for (i = 0; i < 8; i++)
            {
                if ((crc & 0x8000) > 0)
                    // crc = (ushort)((crc << 1) ^ 0x1021); // CRC-CCITT (XModem)
                    // crc = (ushort)((crc << 1) ^ 0xA001); // x16 + x15 + x2 + 1 (CRC-16-ANSI)
                    crc = (ushort)((crc << 1) ^ 0x8005); // x16 + x15 + x2 + 1 (CRC-16-ANSI reversed) 
                else
                    crc <<= 1;
            }
            return crc;
        }

        UInt16 ModRTU_CRC(byte[] buf, int len)
        {
            UInt16 crc = 0xFFFF;

            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

                for (int i = 8; i != 0; i--)
                {    // Loop over each bit
                    if ((crc & 0x0001) != 0)
                    {      // If the LSB is set
                        crc >>= 1;                    // Shift right and XOR 0xA001
                        crc ^= 0xA001;
                    }
                    else                            // Else LSB is not set
                        crc >>= 1;                    // Just shift right
                }
            }
            // Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
            return crc;
        }


     

        private void textbox_shortcut_1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_1);
            }
        }

        private void textbox_shortcut_3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_3);
            }
        }

        private void textbox_shortcut_5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_5);
            }
        }

        private void textbox_shortcut_7_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_7);
            }
        }

        private void textbox_shortcut_9_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_9);
            }
        }

        private void textbox_shortcut_11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_11);
            }
        }

        private void textbox_shortcut_2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_2);
            }
        }

        private void textbox_shortcut_4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_4);
            }
        }

        private void textbox_shortcut_6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_6);
            }
        }

        private void textbox_shortcut_8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_8);
            }
        }

        private void textbox_shortcut_10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonProcessing(textbox_shortcut_10);
            }
        }
    }
}
