using System.Windows;

namespace Codealike.WP8
{
    public partial class App
    {
        public App()
        {
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {            
        }
    }
}