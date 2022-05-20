using System;
using System.Drawing;
using System.Windows.Forms;

namespace SerialTerminal
{
    class _dataGridViewLogger
    {
        private DataGridView dgv_window = null;
        private int dgvRowCount = 0;

        public bool dgvScroolToCarret = true;

        delegate void ClearTextCallback();
        private void ClearText()
        {
            if (this.dgv_window.InvokeRequired)
            {
                try
                {
                    ClearTextCallback d = new ClearTextCallback(ClearText);
                    this.dgv_window.Invoke(d);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("invoke exception : " + ex);
                }
            }
            else
            {
                if (this.dgv_window != null)
                {
                    this.dgvRowCount = 0;
                    this.dgv_window.Rows.Clear();
                }
            }
        }

        delegate void SetTextCallback(string text, Color color);
        private void SetText(string text, Color color)
        {
            if (this.dgv_window.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.dgv_window.Invoke(d, new object[] { text, color });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("invoke exception : " + ex);
                }
            }
            else
            {
                if (this.dgv_window == null || this.dgv_window.ColumnCount < 1)
                {
                    // MessageBox.Show("ERROR", "if (dgw.ColumnCount < 1)");
                    return;
                }

                this.dgv_window.Rows.Add();

                this.dgv_window.Rows[this.dgvRowCount].Cells[0].Value = text;
                this.dgv_window.Rows[this.dgvRowCount].Cells[0].Style.ForeColor = color;

                if (this.dgvScroolToCarret) // enable/disable scrool to carret function in data grid view
                    this.dgv_window.FirstDisplayedScrollingRowIndex = this.dgvRowCount;

                this.dgvRowCount++;
            }
        }

        public void setDataGridTarget(DataGridView dgv)
        {
            this.dgv_window = dgv;
        }

        public void Print(string Text) // printējam tekstus uz debug textboxu/listboxu
        {
            this.Print(Text, Color.CadetBlue);
        }

        public void Clear() // notīram dataggridview objektu
        {
            this.ClearText();
        }

        public void Print(string Text, Color color) // printējam tekstus uz debug textboxu/listboxu
        {
            Text = DateTime.Now.ToString() + " : " + Text;
            this.SetText(Text, color);
        }
    }
}
