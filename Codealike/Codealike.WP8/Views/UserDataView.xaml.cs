using Codealike.WP8.ViewModels;

namespace Codealike.WP8.Views
{
    public partial class UserDataView
    {
        private IUserDataViewModel _viewModel;
        public UserDataView()
        {
            InitializeComponent();
            Loaded += UserDataView_Loaded;
        }

        void UserDataView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel = DataContext as IUserDataViewModel;
            InitializeUI();
        }

        private void InitializeUI()
        {            
            CodingBar.Width = _viewModel.UserData.ActivityPercentage.Coding * 3;
            DebuggingBar.Width = _viewModel.UserData.ActivityPercentage.Debugging * 3;
            BuildingBar.Width = _viewModel.UserData.ActivityPercentage.Building * 3;
        }
    }
}