using System;

namespace Codealike.WP8.Views
{
    using System.Threading.Tasks;
    
    using ViewModels;

    public partial class UserFactsView
    {
        private IUserFactsViewModel _viewModel;
        public UserFactsView()
        {
            InitializeComponent();
            Loaded += UserDataView_Loaded;
        }

        async void UserDataView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel = DataContext as IUserFactsViewModel;
            if (_viewModel != null) await _viewModel.LoadData();
            InitializeUI();
        }

        private async void InitializeUI()
        {
            CodingBar.Width = 0;
            DebuggingBar.Width = 0;
            BuildingBar.Width = 0;
            var progressCount = 50;
            for ( int i = 0; i < progressCount; i++ )
            {
                CodingBar.Width += ( _viewModel.UserData.ActivityPercentage.Coding * 3 ) / progressCount;
                DebuggingBar.Width += ( _viewModel.UserData.ActivityPercentage.Debugging * 3 ) / progressCount;
                BuildingBar.Width += ( _viewModel.UserData.ActivityPercentage.Building * 3 ) / progressCount;

                CodingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Coding * i ) / progressCount ).ToString("F");
                DebuggingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Debugging * i ) / progressCount ).ToString("F");
                BuildingPercentage.Text = ( ( _viewModel.UserData.ActivityPercentage.Building * i ) / progressCount ).ToString("F");
                await Task.Delay(1);
            }
            /*CodingBar.Width = _viewModel.UserData.ActivityPercentage.Coding * 3;
            DebuggingBar.Width = _viewModel.UserData.ActivityPercentage.Debugging * 3;
            BuildingBar.Width = _viewModel.UserData.ActivityPercentage.Building * 3;*/
        }

        private async void RefreshData(object sender, EventArgs e)
        {
            await _viewModel.LoadData();
            InitializeUI();
        }
    }
}