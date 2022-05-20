/**
 * SERIAL PORT APPLICATION 
 * @author Janis Olehnovics <janis.olehnovics@mapon.com>
 * v.0.01
 */

using System;
using System.IO.Ports;
using System.Windows.Forms;

/*
 * v00.01.01 (07.12.2013): first realease 
 * v0 - major version 
 * .01 - functionality related version number 
 * .01 - bugfix related version number
 */

namespace SerialTerminal
{
    public partial class SimpleFormTerminal : Form, ISerialPortView
    {
        public event EventHandler OnOpenCloseButtonPressed;
        public event Action<string> OnSerialPortNameChanged;
        public event Action<string> OnSerialBaudrateChanged;
        public SimpleFormTerminal()
        {
            InitializeComponent();

            comboBox_Port.DataSource = SerialPort.GetPortNames();
        }

        public string SerialPortStatusDisplay 
        { 
            get { return button_openSerialPort.Text; }

            set {
                if (button_openSerialPort.InvokeRequired)
                {
                    Action safeWrite = delegate { SerialPortStatusDisplay = value; };
                    button_openSerialPort.Invoke(safeWrite);
                    return;
                }

                button_openSerialPort.Text = value; 
            }
        }

        private void button_openSerialPort_Click(object sender, EventArgs e)
        {
            OnOpenCloseButtonPressed.Invoke(null, EventArgs.Empty);
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.F3:
                    textbox_ReceiveBox.Clear();
                    break;
                default:
                    return false;
            }
            return false;
        }


        private void comboBox_Port_DropDown(object sender, EventArgs e)
        {
            comboBox_Port.DataSource = SerialPort.GetPortNames();
        }

        private void comboBox_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Disable Manual entries
            e.Handled = true;
        }

        private void comboBox_Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSerialPortNameChanged.Invoke(this.comboBox_Port.Text);
            //Console.WriteLine($"comboBox_Port_SelectedIndexChanged {this.comboBox_Port.Text}");
        }

        private void comboBox_Baudrate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                OnSerialBaudrateChanged?.Invoke(comboBox_Baudrate.Text);
            }
        }

        private void comboBox_Baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSerialBaudrateChanged?.Invoke(comboBox_Baudrate.Text);
        }

        public void AppendSerialData(string data)
        {
            //
        }


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
