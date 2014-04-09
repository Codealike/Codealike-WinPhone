namespace Codealike.WP8.ViewModels
{
    using PortableLogic.Tools;
    using PortableLogic.Communication.ApiModels;

    public class UserDataViewModel : ViewModelBase, IUserDataViewModel
    {
        private readonly IPageNavigationService _pageNavigationService;
        private UserData _userData;

        public UserDataViewModel(IPageNavigationService pageNavigationService) : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UserData = _pageNavigationService.Data["UserData"] as UserData;
        }

        public UserData UserData
        {
            get { return _userData; }
            set
            {
                if (Equals(value, _userData)) return;
                _userData = value;
                NotifyOfPropertyChange(() => UserData);
            }
        }
    }
}
