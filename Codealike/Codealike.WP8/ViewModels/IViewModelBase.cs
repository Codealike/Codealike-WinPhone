namespace Codealike.WP8.ViewModels
{
    public interface IViewModelBase
    {
        string ErrorMessage { get; set; }
        bool ErrorIsVisible { get; set; }
    }
}