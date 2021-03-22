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
    public class RegisterEventUserCommandHandlerTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<RegisterEventUserCommandHandlerRsc>> _mockedLocalizer;
        private readonly RegisterEventUserCommandHandler _commandHandler;

        public RegisterEventUserCommandHandlerTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<RegisterEventUserCommandHandlerRsc>>();

            _commandHandler = new RegisterEventUserCommandHandler(_mockedEventRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = RegisterEventUserCommandFaker.GetRegisterEventUserCommand(); 
            _mockedEventRepository.Setup(x => x.HasEventToUser(200, 300)).Returns(true);
            _mockedEventRepository.Setup(x => x.Register(It.IsAny<SubscriptionModel>())).Verifiable();

            //Act
            var response = _commandHandler.Handle(command, new System.Threading.CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Ok.ToString());
        }

        [Fact]
        public void Handler_WhenHasEventToUser_ReturnShoulBeError()
        {
            //Arrange
            var command = RegisterEventUserCommandFaker.GetRegisterEventUserCommand();
            _mockedEventRepository.Setup(x => x.HasEventToUser(command.EventoId, command.UsuarioId)).Returns(true);
            _mockedEventRepository.Setup(x => x.Register(It.IsAny<SubscriptionModel>())).Verifiable();

            //Act
            var response = _commandHandler.Handle(command, new System.Threading.CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }

        [Fact]
        public void Handler_WhenRepositoryReturnError_ReturnShoulBeException()
        {
            //Arrange
            var command = RegisterEventUserCommandFaker.GetRegisterEventUserCommand();
            _mockedEventRepository.Setup(x => x.HasEventToUser(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}