using Codealike.PortableLogic.Communication.Services;

namespace Codealike.WP8.ViewModels
{
    public class LoginViewModel
    {
        private readonly IUserDataService _userDataService;

        public LoginViewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public string UserName { get; set; }

        public string TokenData { get; set; }

        public async void Login()
        {
            var tokenParts = TokenData.Split('/');
            var identity = tokenParts[0];
            var token = tokenParts[1];
            var credentials = new Credentials();
            credentials.Username = UserName;
            credentials.Token = token;
            credentials.Identity = identity;
            var webApiCallReport = await _userDataService.GetUserData(credentials);
        }
    }
}
