using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Events;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using Poc.Test.ObjectsFakers.Entities;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.Event.CommandHandler
{
    public class DisableEventCommandHandlerTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<DisableEventCommandHandlerRsc>> _mockedLocalizer;
        private readonly DisableEventCommandHandler _commandHandler;

        public DisableEventCommandHandlerTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<DisableEventCommandHandlerRsc>>();

            _commandHandler = new DisableEventCommandHandler(_mockedEventRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var model = EventModelFaker.GetModelValid();
            var command = DisableEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.EventExists(It.IsAny<int>())).Returns(model);
            _mockedEventRepository.Setup(x => x.Cancel(model)).Verifiable(); 

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
            var command = DisableEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.EventExists(It.IsAny<int>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}