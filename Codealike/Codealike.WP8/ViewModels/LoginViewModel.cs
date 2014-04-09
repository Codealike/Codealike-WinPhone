using System;
using System.Threading.Tasks;
using Codealike.PortableLogic.Communication.Services;
using Codealike.PortableLogic.Tools;
using Codealike.WP8.Resources;

namespace Codealike.WP8.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IUserNotificationService _userNotificationService;

        public LoginViewModel(IUserDataService userDataService, IPageNavigationService pageNavigationService, IUserNotificationService userNotificationService)
            : base(pageNavigationService)
        {
            _userDataService = userDataService;
            _pageNavigationService = pageNavigationService;
            _userNotificationService = userNotificationService;
        }

        public string UserName { get; set; }

        public string TokenData { get; set; }

        public async Task Login()
        {
            var credentials = GetCredentials();
            if (credentials == null)
                return;
            var webApiCallReport = await _userDataService.GetUserData(credentials);
            if (webApiCallReport.Successful == false)
                _userNotificationService.ShowError(webApiCallReport.ErrorMessage);
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
