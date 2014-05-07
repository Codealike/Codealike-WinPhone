using Codealike.PortableLogic.Communication.Services;
using Codealike.PortableLogic.Repositories;
using Codealike.PortableLogic.Tools;
using Moq;

namespace Codealike.Tests.TestUtils
{
    public static class TestMocks
    {
        public static Mock<IUserDataService> UserDataServiceMock;
        public static Mock<IPageNavigationService> PageNavigationMock;
        public static Mock<IUserNotificationService> UserNotificationMock;
        public static Mock<IAppRepository> AppRepositoryMock;

        static TestMocks()
        {
            Initialize();
        }

        public static void Initialize()
        {
            UserDataServiceMock = new Mock<IUserDataService>();
            PageNavigationMock = new Mock<IPageNavigationService>();
            UserNotificationMock = new Mock<IUserNotificationService>();
            AppRepositoryMock = new Mock<IAppRepository>();

            NinjectIoC.Kernel.Rebind<IAppRepository>().ToConstant(AppRepositoryMock.Object);
            NinjectIoC.Kernel.Rebind<IUserDataService>().ToConstant(UserDataServiceMock.Object);
            NinjectIoC.Kernel.Rebind<IPageNavigationService>().ToConstant(PageNavigationMock.Object);
            NinjectIoC.Kernel.Rebind<IUserNotificationService>().ToConstant(UserNotificationMock.Object);
        }
    }
}
