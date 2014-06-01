using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Codealike.PortableLogic.Communication.Services;
using Codealike.PortableLogic.Repositories;
using Codealike.WP8.Models;

namespace Codealike.WP8.ViewModels
{
    using PortableLogic.Tools;
    using PortableLogic.Communication.ApiModels;

    public class DaysOnFireViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IUserDataService _userDataService;
        private readonly IAppRepository _appRepository;
        private readonly IUserNotificationService _userNotificationService;
        private UserData _userData;
        private List<MonthHeader> _days;

        public DaysOnFireViewModel(IPageNavigationService pageNavigationService, IUserDataService userDataService, IAppRepository appRepository, IUserNotificationService userNotificationService)
            : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
            _userDataService = userDataService;
            _appRepository = appRepository;
            _userNotificationService = userNotificationService;
            DisplayName = "Days on fire";
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            object data;
            if(UserData != null)
                return;
            
            var userDataIsStored = _pageNavigationService.Data.TryGetValue("UserData", out data);
            if (userDataIsStored)
                UserData = data as UserData;
            if (UserData == null)
                await LoadData();
            if (UserData != null)
            {
                Days = ProcessRides(UserData.DaysOnFire);
            }
        }

        public async Task LoadData()
        {
            IsBusy = true;
            try
            {
                var credentials = _appRepository.LoadCredentials();
                if ( credentials == null )
                    return;
                var webApiCallReport = await _userDataService.GetUserData(credentials);
                if ( webApiCallReport.Successful == false )
                    _userNotificationService.ShowError(webApiCallReport.ErrorMessage);
                else
                {
                    _pageNavigationService.Data["UserData"] = webApiCallReport.Content;
                    UserData = webApiCallReport.Content;
                    _appRepository.SaveCredentials(credentials);
                }
            }
            catch ( Exception )
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        public static List<MonthHeader> ProcessRides(List<DateTime> days)
        {
            var months = new List<MonthHeader>();
            try
            {
                var history =
                days.Where(r => r.Year < DateTime.Now.Year)
                    .OrderByDescending(r => r)
                    .ToList();
                var thisYearsHistory =
                    days.Where(r => r.Year == DateTime.Now.Year && r < DateTime.Now)
                        .OrderByDescending(r => r)
                        .GroupBy(r => r.Month)
                        .Select(
                            g =>
                                new MonthHeader
                                {
                                    Date = new DateTime(DateTime.Now.Year, g.Key, 1),
                                    Days = g.Select(d => d.ToString("D")).ToList(),
                                    Header = GetMonthByNumber(g.Key),
                                    IsHeaderVisible = true
                                })
                        .ToList();
                var groupedRides = history
                    .GroupBy(r => r.Year)
                    .Select(
                        g =>
                            new MonthHeader
                            {
                                Header = g.Key.ToString(CultureInfo.InvariantCulture),
                                Days = g.Select(d => d.ToString("D")).ToList(),
                                Date = new DateTime(g.Key, 1, 1),
                                IsHeaderVisible = true
                            })
                    .ToList();
                var newRides = new MonthHeader
                {
                    Date = DateTime.Now,                    
                    Days = days.Where(r => r > DateTime.Now).Select(d => d.ToString("D")).ToList(),                    
                };

                months.Add(newRides);
                months = months.Concat(thisYearsHistory).ToList();
                months = months.Concat(groupedRides).ToList();
                foreach (var month in months)
                {
                    month.IsHeaderVisible = month.Days.Count > 0;
                    month.Count = month.Days.Count;
                }
            }
            catch ( Exception )
            {
                return new List<MonthHeader>();
            }
            return months;
        }

        private static string GetMonthByNumber(int key)
        {
            return new DateTime(2010, key, 1).ToString("MMMM");
        }

        public List<MonthHeader> Days
        {
            get { return _days; }
            set
            {
                if ( Equals(value, _days) )
                    return;
                _days = value;
                NotifyOfPropertyChange(() => Days);
            }
        }

        public UserData UserData
        {
            get { return _userData; }
            set
            {
                if ( Equals(value, _userData) )
                    return;
                _userData = value;
                NotifyOfPropertyChange(() => UserData);
            }
        }
    }
}
