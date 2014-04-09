using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Caliburn.Micro;
using Codealike.PortableLogic.Communication.Infrastructure;
using Codealike.PortableLogic.Communication.Services;
using Codealike.WP8.ViewModels;
using Microsoft.Phone.Controls;

namespace Codealike.WP8.Tools
{
	public class AppBootstrapper : PhoneBootstrapperBase
	{
		PhoneContainer container;

		public AppBootstrapper()
		{
			Start();
		}

		protected override void Configure()
		{
			container = new PhoneContainer();
			if (!Execute.InDesignMode)
				container.RegisterPhoneServices(RootFrame);

			container.PerRequest<LoginViewModel>();

            container.RegisterPerRequest(typeof(IWebClient), "WebClient", typeof(WebClient));
            container.RegisterPerRequest(typeof(IUserDataService), "UserDataService", typeof(UserDataService));

			AddCustomConventions();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		static void AddCustomConventions()
		{
			ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
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

			ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
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