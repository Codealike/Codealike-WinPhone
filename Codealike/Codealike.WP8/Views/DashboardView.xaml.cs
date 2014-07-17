using System;
using System.Diagnostics;
using Microsoft.Phone.Scheduler;

namespace Codealike.WP8.Views
{
    public partial class DashboardView
    {
        private const string TaskName = "codealike";

        public DashboardView()
        {
            InitializeComponent();
            Loaded += DashboardView_Loaded;
        }

        void DashboardView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PeriodicTileUpdater();
        }

        public static void PeriodicTileUpdater()
        {
            var periodicTask = ScheduledActionService.Find(TaskName) as PeriodicTask;

            if ( periodicTask != null )
            {
                ScheduledActionService.Remove(TaskName);
            }

            periodicTask = new PeriodicTask(TaskName)
            {
                Description = "Periodic Task to Update the ApplicationTile"
            };

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
                //ScheduledActionService.LaunchForTest(TaskName, TimeSpan.FromSeconds(10));
            }
            catch ( InvalidOperationException ex )
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}