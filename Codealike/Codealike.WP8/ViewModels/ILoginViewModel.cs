namespace Codealike.WP8.ViewModels
{
    using System.Threading.Tasks;

    public interface ILoginViewModel: IViewModelBase
    {
        string UserName { get; set; }

        string TokenData { get; set; }
        
        Task Login();
    }
}
