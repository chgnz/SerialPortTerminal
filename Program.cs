using System;
using System.Windows.Forms;

namespace SerialTerminal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var simple_form = new SimpleFormTerminal();
            //var empty_form = new Form1();

            var presenter_main_form = new SerialPortPresenter(simple_form);
            //var presenter_empty_form = new SerialPortPresenter(empty_form);

            //simple_form.Show();
            //empty_form.Show();

            Application.Run(simple_form);
        }
    }
}
