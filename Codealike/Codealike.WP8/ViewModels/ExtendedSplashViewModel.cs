using Codealike.PortableLogic.Repositories;

namespace Codealike.WP8.ViewModels
{
    using PortableLogic.Tools;

    public class ExtendedSplashViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IAppRepository _appRepository;

        public ExtendedSplashViewModel(IPageNavigationService pageNavigationService, IAppRepository appRepository) : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
            _appRepository = appRepository;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            var credentials = _appRepository.LoadCredentials();
            if ( credentials != null )
                _pageNavigationService.NavigateTo<DashboardViewModel>();
            else _pageNavigationService.NavigateTo<LoginViewModel>();
        }
    }
}
