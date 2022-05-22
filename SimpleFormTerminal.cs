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
        public event Action<int> OnUsbDeviceChange;
        public event Action<string> OnSerialDataTransmit;
        public event Action<string> OnLineEndingsChanged;
        public event Action<string, int> OnPeriodicDataToggle;

        public SimpleFormTerminal()
        {
        }

        public void Init()
        {
            InitializeComponent();
            
            comboBox_Port.Items.AddRange(SerialPort.GetPortNames());
            if (comboBox_Port.Items.Count != 0)
            {
                comboBox_Port.SelectedIndex = 0;
            }
        }

        public void TriggerInitialEvents()
        {
            OnSerialPortNameChanged?.Invoke(this.comboBox_Port.Text);
            OnSerialBaudrateChanged?.Invoke(this.comboBox_Baudrate.Text);

            // change radiobutton state, which will trigger event
            this.radioButton_RN.Checked = true;
        }

        public string SerialPortStatusDisplay
        {
            get { return button_openSerialPort.Text; }

            set
            {
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
            string[] ports = SerialPort.GetPortNames();
            comboBox_Port.Items.Clear();
            comboBox_Port.Items.AddRange(ports);
        }

        private void comboBox_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("comboBox_Port_KeyPress");
            // Disable Manual entries
            e.Handled = true;
        }

        private void comboBox_Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("comboBox_Port_SelectedIndexChanged");
            OnSerialPortNameChanged?.Invoke(this.comboBox_Port.Text);
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
            this.textbox_ReceiveBox.AppendText(data);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x0219;

            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    // forward usb device change event to presenter/model class
                    OnUsbDeviceChange?.Invoke((int)m.WParam);
                    break;
            }
            base.WndProc(ref m);
        }

        private void tsbutton_clearTextbox_Click(object sender, EventArgs e)
        {
            textbox_ReceiveBox.Clear();
        }

        private void textbox_SendBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                OnSerialDataTransmit?.Invoke(this.textbox_SendBox.Text);
                this.textbox_SendBox.Clear();
            }
        }

        private void tsbutton_dtrButton_Click(object sender, EventArgs e)
        {
            // use as shortcuts
            this.WindowsSplitContainer.Panel2Collapsed = !this.WindowsSplitContainer.Panel2Collapsed;
        }

        private void textboxButtonClickHandler(object sender, EventArgs e)
        {
            OnSerialDataTransmit?.Invoke((sender as TextboxButton).Text);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedState = (sender as RadioButton).Checked;

            if (!checkedState)
            {
                // ignore uncheck events
                return;
            }

            if ((sender as RadioButton).Equals(this.radioButtonNone))
            {
                OnLineEndingsChanged?.Invoke("");
                return;
            }

            if ((sender as RadioButton).Equals(this.radioButton_N))
            {
                OnLineEndingsChanged?.Invoke("\n");
                return;
            }

            if ((sender as RadioButton).Equals(this.radioButton_R))
            {
                OnLineEndingsChanged?.Invoke("\r");
                return;
            }

            if ((sender as RadioButton).Equals(this.radioButton_RN))
            {
                OnLineEndingsChanged?.Invoke("\r\n");
                return;
            }
        }

        private void PeriodicTransmitControl_Click(object sender, EventArgs e)
        {
            DualTextboxButton dtb = sender as DualTextboxButton;

            int interval_ms;
            if (!int.TryParse(dtb.Interval, out interval_ms))
            {
                dtb.Interval += (" (Err)");
            }

            OnPeriodicDataToggle?.Invoke(dtb.Data, interval_ms);
        }
    }
}
