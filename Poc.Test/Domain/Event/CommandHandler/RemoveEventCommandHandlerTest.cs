using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Events;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.Event.CommandHandler
{
    public class RemoveEventCommandHandlerTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<RemoveEventCommandHandlerRsc>> _mockedLocalizer;
        private readonly RemoveEventCommandHandler _commandHandler;

        public RemoveEventCommandHandlerTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<RemoveEventCommandHandlerRsc>>();

            _commandHandler = new RemoveEventCommandHandler(_mockedEventRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = RemoveEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Remove(command.EventoId)).Verifiable(); 

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
            var command = RemoveEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.Remove(command.EventoId)).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}