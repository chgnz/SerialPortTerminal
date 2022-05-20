using System;
using System.Drawing;
using System.Windows.Forms;

namespace SerialTerminal
{
    static class _updateControl
    {
        delegate void SetTextCallback(string text, object obj, Color color);
        static public void setText(string text, object obj, Color color)
        {
            Control ctrl = (Control)obj;
            if (ctrl.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(setText);
                    ctrl.Invoke(d, new object[] { text, obj, color });
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                } 
            }
                    
            if (obj is TextBox)
            {                
                TextBox textbox = (TextBox)obj;
                if (textbox.IsDisposed == false)
                {
                    textbox.AppendText(text);
                }
            }
            else if (obj is ListBox)
            {
                ListBox listbox = (ListBox)obj;
                if (listbox.IsDisposed == false)
                {
                    listbox.Items.Add(text);
                }
            }
            else if (obj is Button)
            {
                Button button = (Button)obj;
                button.Text = text;
            }
            else if (obj is Label)
            {
                Label lbl = (Label)obj;
                lbl.Text = text;
                lbl.ForeColor = color;
            }
        }
        
       /**
        * <Set safetly texts in textbox(appending text),listbox (adding item), button(changing button.Text)>
        * <example : _updateControl.setText("my Text here" , inputTextbox)> 
        * appends safetly "my Text here" string to inputTextbox
        * 
        * @param  string text - text that will be added to object
        * @param  obj - object that will be changed.
        */
        delegate void ClearTextCallback(object obj);
        public static void clearText(object obj)
        {
            if (obj is TextBox)
            {
                TextBox textbox = (TextBox)obj;

                if (textbox.InvokeRequired)
                {
                    try
                    {
                        ClearTextCallback d = new ClearTextCallback(clearText);
                        textbox.Invoke(d, new object[] { obj });
                    }
                    catch (Exception ex)
                    {
                        //dgvLogger.Print("invoke exception : " + ex, Color.IndianRed);
                    }
                }
                else
                {
                    if (textbox.IsDisposed == false)
                    {
                        textbox.Clear();
                    }
                }
            
            }
        }
        
    }
}
