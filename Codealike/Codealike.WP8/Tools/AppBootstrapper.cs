namespace Codealike.WP8.Tools
{
	using System;
	using System.Windows.Controls;
	using Microsoft.Phone.Controls;
	using System.Collections.Generic;

	using Caliburn.Micro;

    using ViewModels;
    using Repositories;
    using PortableLogic.Tools;
    using PortableLogic.Repositories;
	using PortableLogic.Communication.Services;
	using PortableLogic.Communication.Infrastructure;

	public class AppBootstrapper : PhoneBootstrapperBase
	{
		PhoneContainer _container;

		public AppBootstrapper()
		{
			Start();
		}

		protected override void Configure()
		{
			_container = new PhoneContainer();
			if (!Execute.InDesignMode)
				_container.RegisterPhoneServices(RootFrame);

			_container.PerRequest<LoginViewModel>();
			_container.PerRequest<UserFactsViewModel>();
			_container.PerRequest<ExtendedSplashViewModel>();
			_container.PerRequest<DashboardViewModel>();
			_container.PerRequest<CodeFactsViewModel>();
			_container.PerRequest<DaysOnFireViewModel>();
			_container.PerRequest<HomeViewModel>();

			_container.RegisterPerRequest(typeof(IWebClient), "WebClient", typeof(WebClient));
			_container.RegisterPerRequest(typeof(IUserDataService), "UserDataService", typeof(UserDataService));
			_container.RegisterSingleton(typeof(IPageNavigationService), "PageNavigationService", typeof(PageNavigationService));
			_container.RegisterPerRequest(typeof(IAppRepository), "AppRepository", typeof(AppRepository));
			_container.RegisterPerRequest(typeof(IUserNotificationService), "UserNotificationService", typeof(UserNotificationService));

			AddCustomConventions();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = _container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}

		static void AddCustomConventions()
		{
			ConventionManager.AddElementConvention<Pivot>(ItemsControl.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
				(viewModelType, path, property, element, convention) => {
					if (ConventionManager
						.GetElementConvention(typeof(ItemsControl))
						.ApplyBinding(viewModelType, path, property, element, convention))
					{
						ConventionManager
							.ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
						ConventionManager
							.ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
						return true;
					}

					return false;
				};

			ConventionManager.AddElementConvention<Panorama>(ItemsControl.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
				(viewModelType, path, property, element, convention) => {
					if (ConventionManager
						.GetElementConvention(typeof(ItemsControl))
						.ApplyBinding(viewModelType, path, property, element, convention))
					{
						ConventionManager
							.ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
						ConventionManager
							.ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
						return true;
					}

					return false;
				};
		}
	}
}