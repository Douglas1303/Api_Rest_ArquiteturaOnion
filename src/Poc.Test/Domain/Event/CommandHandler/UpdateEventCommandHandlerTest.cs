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
    public class UpdateEventCommandHandlerTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<UpdateEventCommandHandlerRsc>> _mockedLocalizer;
        private readonly UpdateEventCommandHandler _commandHandler;

        public UpdateEventCommandHandlerTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<UpdateEventCommandHandlerRsc>>();

            _commandHandler = new UpdateEventCommandHandler(_mockedEventRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = UpdateEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Update(It.IsAny<EventModel>())).Verifiable(); 

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
            var command = UpdateEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Update(It.IsAny<EventModel>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}