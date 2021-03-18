using ExternalServices.Cep.Interface;
using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Test.ObjectsFakers.ViewModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserApplication> _mockedUserApplication;
        private readonly Mock<ICepService> _mockedCepService;
        private readonly UserController _userController;
        private readonly List<UserViewModel> _listUserViewModel;

        public UserControllerTest()
        {
            _listUserViewModel = new UserViewModelFaker().Generate(10);

            _mockedUserApplication = new Mock<IUserApplication>();
            _mockedCepService = new Mock<ICepService>();
            _userController = new UserController(_mockedUserApplication.Object, _mockedCepService.Object);
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(_listUserViewModel);

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

        //    //Arrange
        //    _mockedCepService.Setup(x => x.GetAddressAsync("57300180")).ReturnsAsync(new CepModel());
        //    _mockedUserApplication.Setup(x => x.AddAsync(It.IsAny<AddUserViewModel>(), "teste@gmail.com")).ReturnsAsync(commandResult);

        //    //Act
        //    var response = _userController.Add(GetAddUserViewModel());
        //    var objectResult = response.Result as OkObjectResult;
        //    var content = objectResult.Value as IResult;

        //    //Assert
        //    Assert.NotNull(response);
        //    Assert.IsType<OkObjectResult>(objectResult);
        //    Assert.NotEmpty(content.Messages);
        //    Assert.Equal(StatusResult.Ok, content.Status);
        //}

        private AddUserViewModel GetAddUserViewModel()
        {
            return new AddUserViewModel
            {
                NomeCompleto = "Name User",
                Cpf = "12883245029",
                DataNascimento = "01/08/1990",
                Cep = "57300180"
            };
        }
    }
}