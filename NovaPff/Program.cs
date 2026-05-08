using System;
using System.Windows.Forms;

namespace NovaPff
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (s, e) 
                => MessageBox.Show($@"An unexpected error occurred: {e.Exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    MessageBox.Show($@"A fatal error occurred: {ex.Message}",@"Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.Run(new Main());

        }

    }

}