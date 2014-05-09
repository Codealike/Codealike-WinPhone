namespace Codealike.WP8.ViewModels
{
    using Caliburn.Micro;

    public class DashboardViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public DashboardViewModel(UserFactsViewModel userFactsViewModel, CodeFactsViewModel codeFactsViewModel)
        {
            _userFactsViewModel = userFactsViewModel;
            _codeFactsViewModel = codeFactsViewModel;
        }

        private readonly UserFactsViewModel _userFactsViewModel;
        private readonly CodeFactsViewModel _codeFactsViewModel;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(_userFactsViewModel);
            Items.Add(_codeFactsViewModel);
            ActivateItem(_userFactsViewModel);
        }
    }
}
