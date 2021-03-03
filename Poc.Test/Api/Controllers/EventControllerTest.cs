using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class EventControllerTest
    {
        private readonly Mock<IEventApplication> _mockedEventApplication;
        private readonly EventController _eventController;

        public EventControllerTest()
        {
            _mockedEventApplication = new Mock<IEventApplication>();

            _eventController = new EventController(_mockedEventApplication.Object);
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetEventModelValid());

            _mockedEventApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _eventController.GetAll();
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.NotNull(content.Data);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void GetAll_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            IResult result = new CommandResult();
            result.AddErrorMessage("Error");

            _mockedEventApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _eventController.GetAll();
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            ////Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Null(content.Data);
            Assert.Equal(StatusResult.Error, content.Status);
        }

        [Fact]
        public void GetByIdAsync_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetByIdEventModelValid());

            _mockedEventApplication.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(result);

            //Act
            var response = _eventController.GetById(1);
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.NotNull(content.Data);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Add_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedEventApplication.Setup(x => x.AddAsync(It.IsAny<AddEventViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.Add(AddAddEventViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Update_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedEventApplication.Setup(x => x.UpdateAsync(It.IsAny<UpdateEventViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.Update(UpdateEventViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void RegisterUserEvent_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedEventApplication.Setup(x => x.RegisterAsync(It.IsAny<AddUserEventViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.RegisterUserEvent(AddUserEventViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Disable_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedEventApplication.Setup(x => x.CancelAsync(1)).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.Disable(1);
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Remove_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedEventApplication.Setup(x => x.RemoveAsync(1)).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.Remove(1);
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        private List<EventModel> GetEventModelValid()
        {
            return new List<EventModel>
            { 
                new EventModel("BootCamp", "BootCamp para devs DotNet", DateTime.Parse("10/05/2030"), DateTime.Parse("10/06/2030"), 1),
                new EventModel("Aulão", "Aulão sobre banco de dados", DateTime.Parse("10/05/2030"), DateTime.Parse("10/06/2030"), 1)
            }; 
        }

        private EventModel GetByIdEventModelValid()
        {
            return new EventModel(1,"BootCamp", "BootCamp para devs DotNet", DateTime.Parse("10/05/2030"), DateTime.Parse("10/06/2030"), 1);
        }

        private AddEventViewModel AddAddEventViewModelValid()
        {
            return new AddEventViewModel
            {
                Titulo = "BootCamp", 
                Descricao = "BootCamp para devs DotNet",
                DataInicio = "10/05/2030",
                DataFim = "10/06/2030",
                CategoriaId = 1
            }; 
        }

        private UpdateEventViewModel UpdateEventViewModelValid()
        {
            return new UpdateEventViewModel
            {
                Id = 1, 
                Titulo = "BootCamp C#",
                Descricao = "BootCamp para devs .net",
                DataInicio = "10/05/2030",
                DataFim = "10/06/2030",
                CategoriaId = 1
            }; 
        }

        private AddUserEventViewModel AddUserEventViewModelValid()
        {
            return new AddUserEventViewModel
            {
                EventoId = 1,
                UsuarioId = 1
            };
        }
    }
}