using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Test.ObjectsFakers.ViewModel;
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
            IResult result = new QueryResult(EventViewModelFaker.GetListViewModelValid());

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
            IResult result = new QueryResult(EventViewModelFaker.GetViewModelValid());

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
            var viewModel = AddEventViewModelFaker.GetViewModelValid();
            //Arrange
            _mockedEventApplication.Setup(x => x.AddAsync(It.IsAny<AddEventViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _eventController.Add(viewModel);
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
            var response = _eventController.Update(UpdateEventViewModelFaker.GetViewModelValid());
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
            var response = _eventController.RegisterUserEvent(AddUserEventViewModelFaker.GetViewModelValid());
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
    }
}