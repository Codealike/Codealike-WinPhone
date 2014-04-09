using Codealike.PortableLogic.Communication.Services;
using Codealike.PortableLogic.Tools;
using Moq;

namespace Codealike.Tests.TestUtils
{
    public static class TestMocks
    {
        public static Mock<IUserDataService> UserDataServiceMock;
        public static Mock<IPageNavigationService> PageNavigationMock;
        public static Mock<IUserNotificationService> UserNotificationMock;

        static TestMocks()
        {
            Initialize();
        }

        public static void Initialize()
        {
            UserDataServiceMock = new Mock<IUserDataService>();
            PageNavigationMock = new Mock<IPageNavigationService>();
            UserNotificationMock = new Mock<IUserNotificationService>();

            NinjectIoC.Kernel.Rebind<IUserDataService>().ToConstant(UserDataServiceMock.Object);
            NinjectIoC.Kernel.Rebind<IPageNavigationService>().ToConstant(PageNavigationMock.Object);
            NinjectIoC.Kernel.Rebind<IUserNotificationService>().ToConstant(UserNotificationMock.Object);
        }
    }
}
