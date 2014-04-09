using Codealike.PortableLogic.Repositories;

namespace Codealike.Tests.TestUtils
{
    using Moq;
    using Ninject.Modules;

    using WP8.ViewModels;
    using PortableLogic.Tools;
    using PortableLogic.Communication.Services;
    
    public class Bindings: NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginViewModel>().To<LoginViewModel>();
            Bind<IUserDataViewModel>().To<UserDataViewModel>();

            Bind<IUserDataService>().ToConstant(new Mock<IUserDataService>().Object);
            Bind<IPageNavigationService>().ToConstant(new Mock<IPageNavigationService>().Object);
            Bind<IAppRepository>().ToConstant(new Mock<IAppRepository>().Object);
            Bind<IUserNotificationService>().ToConstant(new Mock<IUserNotificationService>().Object);
        }
    }
}
