using ExternalServices.Cep.Interface;
using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
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

        public UserControllerTest()
        {
            _mockedUserApplication = new Mock<IUserApplication>();
            _mockedCepService = new Mock<ICepService>(); 
            _userController = new UserController(_mockedUserApplication.Object, _mockedCepService.Object);
        }


        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetuserViewModelValid());

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

        private List<UserViewModel> GetuserViewModelValid()
        {
            return new List<UserViewModel>
            {
                new UserViewModel
                {
                    Id = 1,
                    DataCadastro = DateTime.Parse("10/01/2021"),
                    NomeCompleto = "Name User",
                    Cpf = "12883245029",
                    DataNascimento = DateTime.Parse("01/08/1990"),
                    Email = "teste@gmail.com",
                    Ativo = true
                },
                new UserViewModel
                {
                    Id = 2,
                    DataCadastro = DateTime.Parse("05/10/2020"),
                    NomeCompleto = "Example User",
                    Cpf = "10360575005",
                    DataNascimento = DateTime.Parse("01/08/2000"),
                    Email = "teste2@gmail.com",
                    Ativo = true
                }
            }; 
        }
    }
}