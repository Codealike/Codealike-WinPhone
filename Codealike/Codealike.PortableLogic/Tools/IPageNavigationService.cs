using System.Collections.Generic;

namespace Codealike.PortableLogic.Tools
{
    public interface IPageNavigationService
    {
        bool CanGoBack { get; set; }

        Dictionary<string, object> Data { get; set; }
        void NavigateTo<T>();
        void GoBack();
        void CloseApp();
    }
}
