using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface.Identity;
using Poc.Application.ViewModel.Identity;
using Poc.Test.ObjectsFakers.ViewModel;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class AuthorizationControllerTest
    {
        private readonly Mock<IAuthorizationApplication> _mockedAuthorizationApplication;
        private readonly AuthorizationController _authorizationController;

        public AuthorizationControllerTest()
        {
            _mockedAuthorizationApplication = new Mock<IAuthorizationApplication>();

            _authorizationController = new AuthorizationController(_mockedAuthorizationApplication.Object);
        }

        [Fact]
        public void RegisterUser_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedAuthorizationApplication.Setup(x => x.RegisterUserAsync(It.IsAny<UserIdentityViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _authorizationController.RegisterUser(UserIdentityViewModelFaker.GetViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void RegisterUser_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            IResult commandResult = new CommandResult();
            commandResult.AddErrorMessage("Error");

            //Arrange
            _mockedAuthorizationApplication.Setup(x => x.RegisterUserAsync(It.IsAny<UserIdentityViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _authorizationController.RegisterUser(UserIdentityViewModelFaker.GetViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Error, content.Status);
        }

        [Fact]
        public void Login_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedAuthorizationApplication.Setup(x => x.LoginAsync(It.IsAny<LoginIdentityViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _authorizationController.Login(LoginIdentityViewModelFaker.GetViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Login_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            IResult commandResult = new CommandResult();
            commandResult.AddErrorMessage("Error");

            //Arrange
            _mockedAuthorizationApplication.Setup(x => x.LoginAsync(It.IsAny<LoginIdentityViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _authorizationController.Login(LoginIdentityViewModelFaker.GetViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Error, content.Status);
        }
    }
}