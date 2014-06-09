namespace Codealike.WP8.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Resources;
    using PortableLogic.Tools;
    using PortableLogic.Repositories;
    using PortableLogic.Communication.Services;
    
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IAppRepository _appRepository;

        public LoginViewModel(IUserDataService userDataService, IPageNavigationService pageNavigationService, IUserNotificationService userNotificationService, IAppRepository appRepository)
            : base(pageNavigationService)
        {
            _userDataService = userDataService;
            _pageNavigationService = pageNavigationService;
            _userNotificationService = userNotificationService;
            _appRepository = appRepository;
            TokenData = "bogdan/aa8d8752-7404-46ab-b55f-4b40fc8b60e7";
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            var credentials = _appRepository.LoadCredentials();
            if ( credentials == null )
                return;
            await LoginUser(credentials);
        }

        public string UserName { get; set; }

        public string TokenData { get; set; }

        public async Task Login()
        {
            var credentials = GetCredentials();
            if (credentials == null)
                return;
            await LoginUser(credentials);
        }

        private async Task LoginUser(Credentials credentials)
        {
            IsBusy = true;
            try
            {
                var webApiCallReport = await _userDataService.GetUserData(credentials);
                if ( webApiCallReport.Successful == false )
                    _userNotificationService.ShowError(webApiCallReport.ErrorMessage);
                else
                {
                    _pageNavigationService.Data["UserData"] = webApiCallReport.Content;
                    _appRepository.SaveCredentials(credentials);
                    _pageNavigationService.NavigateTo<DashboardViewModel>();
                }
            }
            catch (Exception)
            {
            }
            IsBusy = false;
        }

        private Credentials GetCredentials()
        {
            var credentials = new Credentials();
            try
            {
                if ( UserName.IsNullOrEmptyOrWhiteSpace() )
                {
                    ErrorMessage = "The username field should not be empty";
                    return null;
                }
                if (TokenData.IsNullOrEmptyOrWhiteSpace())
                {
                    ErrorMessage = AppResources.InvalidToken;
                    return null;
                }
                var tokenParts = TokenData.Split('/');
                if ( tokenParts.Length != 2 || tokenParts[0].IsNullOrEmptyOrWhiteSpace() || tokenParts[1].IsNullOrEmptyOrWhiteSpace() )
                {
                    ErrorMessage = AppResources.InvalidToken;
                    return null;
                }
                var identity = tokenParts[0];
                var token = tokenParts[1];
                credentials.Username = UserName;
                credentials.Token = token;
                credentials.Identity = identity;
            }
            catch ( Exception )
            {
                return null;
            }
            return credentials;
        }
    }
}
