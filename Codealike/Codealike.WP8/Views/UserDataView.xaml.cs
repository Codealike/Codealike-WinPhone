namespace Codealike.WP8.Views
{
    using System.Threading.Tasks;
    
    using ViewModels;

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

        private async void InitializeUI()
        {
            CodingBar.Width = 0;
            DebuggingBar.Width = 0;
            BuildingBar.Width = 0;
            for ( int i = 0; i < 100; i++ )
            {
                CodingBar.Width += ( _viewModel.UserData.ActivityPercentage.Coding * 3 ) / 100;
                DebuggingBar.Width += ( _viewModel.UserData.ActivityPercentage.Debugging * 3 ) / 100;
                BuildingBar.Width += ( _viewModel.UserData.ActivityPercentage.Building * 3 ) / 100;

                CodingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Coding * i ) / 100 ).ToString("F");
                DebuggingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Debugging * i ) / 100 ).ToString("F");
                BuildingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Building * i ) / 100 ).ToString("F");
                await Task.Delay(1);
            }
            /*CodingBar.Width = _viewModel.UserData.ActivityPercentage.Coding * 3;
            DebuggingBar.Width = _viewModel.UserData.ActivityPercentage.Debugging * 3;
            BuildingBar.Width = _viewModel.UserData.ActivityPercentage.Building * 3;*/
        }
    }
}