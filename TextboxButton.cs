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
    public partial class TextboxButton : UserControl
    {
        public override string Text { 
            get 
            { 
                return this.textBox.Text; 
            }
            set 
            { this.textBox.Text = value; 
            }
        }

        public TextboxButton()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            return;
        }

        private void TextboxButton_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
