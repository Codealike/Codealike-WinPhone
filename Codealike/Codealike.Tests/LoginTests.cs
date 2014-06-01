using System.Collections.Generic;

namespace Codealike.Tests
{
    using Moq;
    using NUnit.Framework;
    using FluentAssertions;

    using TestUtils;
    using WP8.Resources;
    using WP8.ViewModels;
    using PortableLogic.Communication.Services;
    using PortableLogic.Communication.ApiModels;
    using PortableLogic.Communication.Infrastructure;

    public class LoginTests
    {
        private ILoginViewModel _viewModel;
        private Dictionary<string, object> _dictionary;

        [SetUp]
        public void SetupTest()
        {
            _dictionary = new Dictionary<string, object>();
            TestMocks.Initialize();
            TestMocks.PageNavigationMock.Setup(p => p.Data).Returns(_dictionary);
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
            var viewModelBase = _viewModel as ViewModelBase;
            if (viewModelBase != null) viewModelBase.ErrorMessage.Should().NotBeEmpty();
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

        [Test]
        public async void successfull_data_retrieval_should_navigate_to_data_view_model()
        {
            BindToSuccessfulLogin();
            await _viewModel.Login();
            TestMocks.PageNavigationMock.Verify(p => p.NavigateTo<HomeViewModel>(), Times.Once);
        }

        [Test]
        public async void successfull_data_retrieval_should_pass_data_in_navigation_service()
        {
            BindToSuccessfulLogin();
            await _viewModel.Login();
            _dictionary.Count.Should().Be(1);
            _dictionary["UserData"].Should().NotBeNull();
        }

        private void BindToSuccessfulLogin()
        {
            TestMocks.UserDataServiceMock.Setup(u => u.GetUserData(It.IsAny<Credentials>()))
                .ReturnsTask(new WebApiCallReport<UserData>
                {
                    Successful = true,
                    Content = new UserData()
                });
            _viewModel.TokenData = "test/token";
            _viewModel.UserName = "test";
        }
    }
}
