using Caliburn.Micro;
using Codealike.PortableLogic.Tools;

namespace Codealike.WP8.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        private readonly IPageNavigationService _pageNavigationService;
        private string _errorMessage;
        private bool _errorIsVisible;
        private bool _isBusy;

        protected ViewModelBase(IPageNavigationService pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
        }

        public bool Loaded { get; set; }

        public bool CanGoBack
        {
            get { return _pageNavigationService.CanGoBack; }
        }

        public void GoBack()
        {
            _pageNavigationService.GoBack();
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if ( value == _errorMessage )
                    return;
                _errorMessage = value;
                ErrorIsVisible = true;
                NotifyOfPropertyChange("ErrorMessage");
            }
        }

        public bool ErrorIsVisible
        {
            get { return _errorIsVisible; }
            set
            {
                if ( value.Equals(_errorIsVisible) )
                    return;
                _errorIsVisible = value;
                NotifyOfPropertyChange("ErrorIsVisible");
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if ( value.Equals(_isBusy) )
                    return;
                _isBusy = value;
                NotifyOfPropertyChange("IsBusy");
            }
        }
    }
}