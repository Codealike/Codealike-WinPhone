using Codealike.PortableLogic.Communication.ApiModels;

namespace Codealike.WP8.ViewModels
{
    public interface IUserDataViewModel: IViewModelBase
    {
        UserData UserData { get; set; }
    }
}
