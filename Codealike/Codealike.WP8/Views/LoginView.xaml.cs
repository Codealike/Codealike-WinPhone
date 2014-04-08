using System;
using System.Windows;
using System.Windows.Input;
using Windows.System;

namespace Codealike.WP8.Views
{
    public partial class LoginView
    {
        private bool _helpIsNotOpened;

        public LoginView()
        {
            InitializeComponent();
            Loaded += LoginViewLoaded;
        }

        private void LoginViewLoaded(object sender, RoutedEventArgs e)
        {
            _helpIsNotOpened = true;
        }


        private void OnHelpTapped(object sender, GestureEventArgs e)
        {
            if (_helpIsNotOpened)
            {
                HelpAnimation.Begin();
            }
            else
                HelpText.Opacity = 0;
            _helpIsNotOpened = !_helpIsNotOpened;
        }

        private void OpenSettingsPage(object sender, GestureEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://codealike.com/Settings/Account"));
        }
    }
}