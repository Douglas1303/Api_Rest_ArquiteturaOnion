using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Test.ObjectsFakers.ViewModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class EventApplicationTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IMediatorHandler> _mockedMediatorHandler;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly Mock<IStringLocalizer<EventAppRsc>> _mockedLocalizer;
        private readonly EventApplication _eventApplication;

        private readonly AddEventViewModel _addEventViewModel;
        private readonly UpdateEventViewModel _updateEventViewModel;
        private readonly AddUserEventViewModel _addUserEventViewModel; 

        public EventApplicationTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedMediatorHandler = new Mock<IMediatorHandler>();
            _mockedLog = new Mock<ILogModel>();
            _mockedLocalizer = new Mock<IStringLocalizer<EventAppRsc>>();

            _addEventViewModel = new AddEventViewModelFaker().Generate();
            _updateEventViewModel = new UpdateEventViewModelFaker().Generate();
            _addUserEventViewModel = new AddUserEventViewModelFaker().Generate(); 

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);

            _eventApplication = new EventApplication(_mockedEventRepository.Object, _mockedMediatorHandler.Object, _mapperFake,
                                                     _mockedLog.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(GetListValidEventModel());

            //Act
            var requestResult = _eventApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetAllAsync()).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetByIdAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(GetValidEventModel());

            //Act
            var requestResult = _eventApplication.GetByIdAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public void GetByIdAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetByIdAsync(1)).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.GetByIdAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public void GetByIdAsync_WhenEventIsNull_ReturnShouldBeError()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.GetByIdAsync(22)).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.GetByIdAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedEventRepository.Verify(x => x.GetByIdAsync(22), Times.Never);
        }

        [Fact]
        public void AddAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedEventRepository.Setup(x => x.Add(It.IsAny<EventModel>())).Verifiable();

            //Act
            var requestResult = _eventApplication.AddAsync(_addEventViewModel);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void AddAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.AddAsync(_addEventViewModel);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }

        [Fact]
        public void UpdateAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedEventRepository.Setup(x => x.Update(It.IsAny<EventModel>())).Verifiable();

            //Act
            var requestResult = _eventApplication.UpdateAsync(_updateEventViewModel);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void UpdateAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.UpdateAsync(_updateEventViewModel);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }

        [Fact]
        public void RegisterAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedEventRepository.Setup(x => x.Register(It.IsAny<SubscriptionModel>())).Verifiable();

            //Act
            var requestResult = _eventApplication.RegisterAsync(_addUserEventViewModel);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void RegisterAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.RegisterAsync(_addUserEventViewModel);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }

        [Fact]
        public void CancelAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedEventRepository.Setup(x => x.Cancel(It.IsAny<EventModel>())).Verifiable();

            //Act
            var requestResult = _eventApplication.CancelAsync(1);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void CancelAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.CancelAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }

        [Fact]
        public void RemoveAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).ReturnsAsync(commandResult);
            _mockedEventRepository.Setup(x => x.Remove(1)).Verifiable();

            //Act
            var requestResult = _eventApplication.RemoveAsync(1);

            // Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok.ToString(), requestResult.Result.Status.ToString());
        }

        [Fact]
        public void RemoveAsync_WhenMediatorReturnException_ReturnShouldBeError()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediatorHandler.Setup(x => x.SendCommand(It.IsAny<Command>())).Throws(new Exception());

            //Act
            var requestResult = _eventApplication.RemoveAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }

        private List<EventModel> GetListValidEventModel()
        {
            return new List<EventModel>
            {
                new EventModel("Aulão", "Aulão de banco de dados", DateTime.Parse("10/01/2030"), DateTime.Parse("14/01/2030"), 1),
                new EventModel("Bootcamp", "Bootcamp DotNet", DateTime.Parse("10/01/2030"), DateTime.Parse("14/01/2030"), 1)
            };
        }

        private EventModel GetValidEventModel()
        {
            return new EventModel("Aulão", "Aulão de banco de dados", DateTime.Parse("10/01/2030"), DateTime.Parse("14/01/2030"), 1);
        }
    }
}