using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Codealike.PortableLogic.Communication.ApiModels;
using Codealike.PortableLogic.Communication.Infrastructure;
using Codealike.PortableLogic.Communication.Services;
using Codealike.WP8.Repositories;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace Codealike.LiveUpdates
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if ( Debugger.IsAttached )
            {
                Debugger.Break();
            }
        }

        public void StartLiveTile(UserData userData)
        {
            try
            {
                var tileToFind = ShellTile.ActiveTiles.First();

                if ( tileToFind != null )
                {
                    var newTileData = new FlipTileData
                    {
                        Title = "",
                        Count = 0,
                        BackTitle = "",
                        BackBackgroundImage = new Uri("/TileAssets/tile.png", UriKind.RelativeOrAbsolute),
                        WideBackBackgroundImage = new Uri("/TileAssets/tile.png", UriKind.RelativeOrAbsolute),
                    };
                    newTileData.BackContent = "Coding " + userData.ActivityPercentage.Coding + "%" + Environment.NewLine + "Debugging " + userData.ActivityPercentage.Debugging + "%" + Environment.NewLine + "Building " + userData.ActivityPercentage.Building + "%";
                    tileToFind.Update(newTileData);
                }
            }
            catch ( Exception )
            {

            }
        }

        protected async override void OnInvoke(ScheduledTask task)
        {
            var dataService = new UserDataService(new WebClient());
            var appRepository = new AppRepository();
            var credentials = appRepository.LoadCredentials();
            var apiCallReport = await dataService.GetUserData(credentials);
            if (apiCallReport.Successful)
            {
                StartLiveTile(apiCallReport.Content);
            }
            NotifyComplete();
        }
    }
}