using Codealike.PortableLogic.Communication.ApiModels;
using Codealike.PortableLogic.Communication.Infrastructure;
using Codealike.PortableLogic.Communication.Services;
using Codealike.Tests.TestUtils;
using Codealike.WP8.Resources;
using Codealike.WP8.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Codealike.Tests
{
    public class LoginTests
    {
        private ILoginViewModel _viewModel;
        private Credentials _validCredentials;

        [SetUp]
        public void SetupTest()
        {
            TestMocks.Initialize();
            _viewModel = NinjectIoC.Get<ILoginViewModel>();
            TestMocks.UserDataServiceMock.Setup(u => u.GetUserData(It.IsAny<Credentials>())).ReturnsTask(new WebApiCallReport<UserData>());
        }

        [Test]
        public async void login_should_get_data_if_user_data_is_valid()
        {
            TestMocks.UserDataServiceMock.Setup(u => u.GetUserData(It.IsAny<Credentials>())).ReturnsTask(new WebApiCallReport<UserData>
            {
                Successful = false,
                ErrorMessage = "error"
            });
            _viewModel.TokenData = "test/token";
            _viewModel.UserName = "test";
            await _viewModel.Login();
            
            TestMocks.UserDataServiceMock.Verify(u => u.GetUserData(It.IsAny<Credentials>()), Times.Once);            
        }

        [TestCase("", "user")]
        [TestCase("token", "")]
        [TestCase("", "")]
        public async void invalid_user_data_should_show_error(string token, string userName)
        {
            _viewModel.TokenData = token;
            _viewModel.UserName = userName;
            await _viewModel.Login();
            (_viewModel as ViewModelBase).ErrorMessage.Should().NotBeEmpty();
        }

        [Test]
        public async void invalid_user_data_should_show_not_get_user_data()
        {
            _viewModel.TokenData = "/token";
            _viewModel.UserName = "name";
            await _viewModel.Login();
            TestMocks.UserDataServiceMock.Verify(u => u.GetUserData(It.IsAny<Credentials>()), Times.Never);            
        }

        [Test]
        public async void login_with_forbidden_request_should_show_invalid_data_message()
        {
            TestMocks.UserDataServiceMock.Setup(u => u.GetUserData(It.IsAny<Credentials>())).ReturnsTask(new WebApiCallReport<UserData>
            {
                Successful = false,
                ErrorMessage = AppResources.InvalidOrRestrictedData
            });
             _viewModel.TokenData = "test/token";
            _viewModel.UserName = "test";
            await _viewModel.Login();            
            TestMocks.UserDataServiceMock.Verify(u => u.GetUserData(It.IsAny<Credentials>()), Times.Once);
            TestMocks.UserNotificationMock.Verify(u => u.ShowError(AppResources.InvalidOrRestrictedData));
        }
    }
}
