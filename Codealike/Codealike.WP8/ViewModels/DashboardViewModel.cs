using Caliburn.Micro;

namespace Codealike.WP8.ViewModels
{
    public class DashboardViewModel: Conductor<IScreen>.Collection.OneActive
    {
        public DashboardViewModel(UserFactsViewModel userFactsViewModel, DaysOnFireViewModel daysOnFireViewModel)
        {
            _userFactsViewModel = userFactsViewModel;
            _daysOnFireViewModel = daysOnFireViewModel;
        }
        private readonly UserFactsViewModel _userFactsViewModel;
        private readonly DaysOnFireViewModel _daysOnFireViewModel;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(_userFactsViewModel);
            Items.Add(_daysOnFireViewModel);
            ActivateItem(_userFactsViewModel);
        }
    }
}
