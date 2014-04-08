using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Codealike.PortableLogic.Tools;

namespace Codealike.WP8.Tools
{
    public class PageNavigationService : IPageNavigationService
    {
        private readonly INavigationService _navigationService;

        public PageNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Data = new Dictionary<string, object>();
        }

        public void NavigateTo<T>()
        {
            _navigationService.UriFor<T>().Navigate();
        }

        public void GoBack()
        {
            if ( CanGoBack )
                _navigationService.GoBack();
            else
                CloseApp();
        }

        public bool CanGoBack
        {
            get { return _navigationService.CanGoBack; }
            set { }
        }

        public void CloseApp()
        {
            Application.Current.Terminate();
        }

        public Dictionary<string, object> Data { get; set; }
    }
}