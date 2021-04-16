using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Events;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.Event.CommandHandler
{
    public class AddEventCommandHandlerTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<AddEventCommandHandlerRsc>> _mockedLocalizer;
        private readonly AddEventCommandHandler _commandHandler;

        public AddEventCommandHandlerTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<AddEventCommandHandlerRsc>>(); 

            _commandHandler = new AddEventCommandHandler(_mockedEventRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = AddEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Add(It.IsAny<EventModel>())).Verifiable();

            //Act
            var response = _commandHandler.Handle(command, new System.Threading.CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Ok.ToString());
        }

        [Fact]
        public void Handler_WhenRepositoryReturnError_ReturnShoulBeException()
        {
            //Arrange
            var command = AddEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Add(It.IsAny<EventModel>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}