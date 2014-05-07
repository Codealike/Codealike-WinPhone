using System.Threading.Tasks;
using Codealike.PortableLogic.Communication.ApiModels;

namespace Codealike.WP8.ViewModels
{
    public interface IUserFactsViewModel: IViewModelBase
    {
        UserData UserData { get; set; }
        Task LoadData();
    }
}
