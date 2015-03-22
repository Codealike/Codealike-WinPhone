using Microsoft.Phone.Tasks;

namespace Codealike.WP8.ViewModels
{
    using System.Threading.Tasks;

    using PortableLogic.Tools;
    using PortableLogic.Repositories;

    public class ExtendedSplashViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IAppRepository _appRepository;
        private readonly IUserNotificationService _userNotificationService;

        public ExtendedSplashViewModel(IPageNavigationService pageNavigationService, IAppRepository appRepository, IUserNotificationService userNotificationService) : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
            _appRepository = appRepository;
            _userNotificationService = userNotificationService;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            await Task.Delay(500);
            var credentials = _appRepository.LoadCredentials();
            if ( credentials != null )
                _pageNavigationService.NavigateTo<DashboardViewModel>();
            else _pageNavigationService.NavigateTo<LoginViewModel>();
        }

        public async Task<bool> RescheduleRating(bool ratingShown)
        {
            if ( ratingShown == false )
            {
                var sendFeedback = _userNotificationService.AskQuestion(
                        "Would you like to send us some feedback instead through email?", "feedback");
                if ( sendFeedback )
                {
                    EmailComposeTask task = new EmailComposeTask();
                    task.To = "bujdeabogdan@gmail.com";
                    task.Subject = "Codealike Feedback";
                    task.Show();
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
