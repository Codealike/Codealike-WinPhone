namespace Codealike.WP8.Views
{
    using System.Windows;

    using AppPromo;
    using ViewModels;

    public partial class ExtendedSplashView
    {
        private ExtendedSplashViewModel _viewModel;
        public ExtendedSplashView()
        {
            InitializeComponent();
            Loaded += ViewLoaded;
        }

        private void ViewLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as ExtendedSplashViewModel;
        }

        private async void OnTryReminderCompleted(object sender, RateReminderResult e)
        {
            if ( e.Runs == 5 )
            {
                if(_viewModel == null)
                    _viewModel = DataContext as ExtendedSplashViewModel;
                var reschedule = await _viewModel.RescheduleRating(e.RatingShown);
                if ( reschedule )
                {
                    RateReminder.ResetCounters();
                    RateReminder.RunsBeforeReminder = 5;
                }
            }
        }
    }
}