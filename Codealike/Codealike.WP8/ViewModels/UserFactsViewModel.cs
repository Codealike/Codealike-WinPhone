namespace Codealike.WP8.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using PortableLogic.Tools;
    using PortableLogic.Repositories;
    using PortableLogic.Communication.Services;
    using PortableLogic.Communication.ApiModels;

    public class UserFactsViewModel : ViewModelBase, IUserFactsViewModel
    {
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IUserDataService _userDataService;
        private readonly IAppRepository _appRepository;
        private readonly IUserNotificationService _userNotificationService;
        private UserData _userData;
        private bool _isLoaded;

        public UserFactsViewModel(IPageNavigationService pageNavigationService, IUserDataService userDataService,
            IAppRepository appRepository, IUserNotificationService userNotificationService)
            : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
            _userDataService = userDataService;
            _appRepository = appRepository;
            _userNotificationService = userNotificationService;
            DisplayName = "Behavior facts";
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            try
            {
                UserData = _pageNavigationService.Data["UserData"] as UserData;
                IsLoaded = true;
            }
            catch ( Exception )
            {

            }
        }

        public UserData UserData
        {
            get { return _userData; }
            set
            {
                if ( Equals(value, _userData) )
                    return;
                _userData = value;
                NotifyOfPropertyChange(() => UserData);
            }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                if ( value.Equals(_isLoaded) )
                    return;
                _isLoaded = value;
                NotifyOfPropertyChange(() => IsLoaded);
            }
        }

        public async Task LoadData()
        {
            IsBusy = true;
            try
            {
                var credentials = _appRepository.LoadCredentials();
                if ( credentials == null )
                    return;
                var webApiCallReport = await _userDataService.GetUserData(credentials);
                if ( webApiCallReport.Successful == false )
                    _userNotificationService.ShowError(webApiCallReport.ErrorMessage);
                else
                {
                    _pageNavigationService.Data["UserData"] = webApiCallReport.Content;
                    IsLoaded = true;
                    UserData = webApiCallReport.Content;
                    _appRepository.SaveCredentials(credentials);
                }
            }
            catch ( Exception )
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void BackButtonPressed(CancelEventArgs eventArgs)
        {
            eventArgs.Cancel = true;
            _pageNavigationService.CloseApp();
        }
    }
}