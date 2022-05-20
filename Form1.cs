using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialTerminal
{
    public partial class Form1 : Form
    {
        static int counter;

        public Form1()
        {
            InitializeComponent();

            var timer = new Timer();

            timer.Interval = 1000;
            timer.Start();
            timer.Tick += (sender, e) => { this.label1.Text = Form1.counter++.ToString(); };
        }

        public string SerialPortStatusDisplay { 
            get => this.label1.Text; 
            set => this.label1.Text = value; 
        }

        public event EventHandler OnOpenCloseButtonPressed;

        public event Action<string> OnSerialPortNameChanged;
        public event Action<string> OnSerialBaudrateChanged;
    }
}
