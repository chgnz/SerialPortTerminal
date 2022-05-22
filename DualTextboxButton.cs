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
    public partial class DualTextboxButton : UserControl
    {
        public string GroupBoxText
        {
            get
            {
                return this.groupBox.Text;
            }
            set
            {
                this.groupBox.Text = value;
            }
        }

        public string Interval
        {
            get
            {
                return this.textBoxInterval.Text;
            }
            set
            {
                this.textBoxInterval.Text = value;
            }
        }

        public string Data
        {
            get
            {
                return this.textBoxData.Text;
            }
            set
            {
                this.textBoxData.Text = value;
            }
        }

        public DualTextboxButton()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender != this.button)
                return;

            this.OnClick(e);
        }
    }
}
