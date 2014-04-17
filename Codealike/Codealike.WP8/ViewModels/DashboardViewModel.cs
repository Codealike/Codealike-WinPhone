namespace Codealike.WP8.ViewModels
{
    using Caliburn.Micro;

    public class DashboardViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public DashboardViewModel(UserFactsViewModel userFactsViewModel, CodeFactsViewModel codeFactsViewModel, DaysOnFireViewModel daysOnFireViewModel)
        {
            _userFactsViewModel = userFactsViewModel;
            _codeFactsViewModel = codeFactsViewModel;
            _daysOnFireViewModel = daysOnFireViewModel;
        }
        private readonly UserFactsViewModel _userFactsViewModel;
        private readonly CodeFactsViewModel _codeFactsViewModel;
        private readonly DaysOnFireViewModel _daysOnFireViewModel;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(_userFactsViewModel);
            Items.Add(_codeFactsViewModel);
            Items.Add(_daysOnFireViewModel);
            ActivateItem(_userFactsViewModel);
        }
    }
}
