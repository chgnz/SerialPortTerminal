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
            simple_form.Init();
            var presenter_main_form = new SerialPortPresenter(simple_form);
            simple_form.TriggerInitialEvents();

            Application.Run(simple_form);
        }
    }
}
