using ExternalServices.Cep.Interface;
using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Helper.Interface;
using Poc.Test.ObjectsFakers.ViewModel;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserApplication> _mockedUserApplication;
        private readonly Mock<ICepService> _mockedCepService;
        private readonly Mock<IAuthenticatedUser> _mockedAuthenticatedUser; 
        private readonly Mock<ControllerContext> _mockedControllerContext;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _mockedUserApplication = new Mock<IUserApplication>();
            _mockedCepService = new Mock<ICepService>();
            _mockedAuthenticatedUser = new Mock<IAuthenticatedUser>(); 
            _mockedControllerContext = new Mock<ControllerContext>();
            _userController = new UserController(_mockedUserApplication.Object, _mockedCepService.Object, _mockedAuthenticatedUser.Object);
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(UserViewModelFaker.GetListViewModelValid());

            _mockedUserApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _userController.GetAll();
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.NotNull(content.Data);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        //[Fact]
        //public void Add_WhenServiceIsValid_ReturnShouldBeOk()
        //{
        //    IResult commandResult = new CommandResult();
        //    var viewModel = AddUserViewModelFaker.GetViewModelValid();

        //    //Arrange
        //    _mockedCepService.Setup(x => x.GetAddressAsync(viewModel.Cep)).ReturnsAsync(new CepModel());
        //    _mockedControllerContext.Setup(x => x.HttpContext.User.Identity.GetEmailUser()).Returns("teste@gmail.com");
        //    _mockedUserApplication.Setup(x => x.AddAsync(It.IsAny<AddUserViewModel>(), "teste@gmail.com")).ReturnsAsync(commandResult);

        //    //Act
        //    var response = _userController.Add(viewModel);
        //    var objectResult = response.Result as OkObjectResult;
        //    var content = objectResult.Value as IResult;

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.IsType<OkObjectResult>(objectResult);
        //    Assert.NotEmpty(content.Messages);
        //    Assert.Equal(StatusResult.Ok, content.Status);
        //}
    }
}