namespace Codealike.WP8.Views
{
    using System;
    using System.Windows;
    using Windows.System;
    using System.Windows.Input;

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

        private async void OpenSettingsPage(object sender, GestureEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://codealike.com/Settings/Account"));
        }
    }
}