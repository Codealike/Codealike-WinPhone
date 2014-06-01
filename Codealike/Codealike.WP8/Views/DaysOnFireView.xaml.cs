using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Animation;
using Caliburn.Micro.BindableAppBar;

namespace Codealike.WP8.Views
{
    public partial class DaysOnFireView
    {
        public DaysOnFireView()
        {
            InitializeComponent();
        }

        private void OnHeaderTapped(object sender, GestureEventArgs e)
        {
            HideOrShowList(sender);
        }

        private void HideOrShowList(object sender)
        {
            var grid = sender as Grid;
            if ( grid != null )
            {
                var stackPanel = grid.Parent as StackPanel;
                if ( stackPanel != null )
                {
                    var listBox = stackPanel.FindChildOfType<ListBox>();
                    if ( listBox.Visibility == Visibility.Collapsed )
                    {
                        var openStoryboard = listBox.FindName("ListBoxOpen") as Storyboard;
                        if ( openStoryboard != null )
                            openStoryboard.Begin();
                    }
                    else
                    {
                        var closeStoryboard = listBox.FindName("ListBoxClose") as Storyboard;
                        if ( closeStoryboard != null )
                            closeStoryboard.Begin();
                    }
                }
            }
        }
    }
}