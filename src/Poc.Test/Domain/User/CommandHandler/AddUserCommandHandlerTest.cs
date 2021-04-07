using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Users;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.User.CommandHandler
{
    public class AddUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<AddUserCommandHandlerRsc>> _mockedLocalizer;
        private readonly AddUserCommandHandler _commandHandler;

        public AddUserCommandHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<AddUserCommandHandlerRsc>>();

            _commandHandler = new AddUserCommandHandler(_mockedUserRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange 
            var command = AddUserCommandFaker.GetCommandValid();
            _mockedUserRepository.Setup(x => x.Add(It.IsAny<UserModel>())).Verifiable();

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Ok.ToString()); 
        }

        [Fact]
        public void Handler_WhenRepositoryReturnError_ReturnShoulBeException()
        {
            //Arrange
            var command = AddUserCommandFaker.GetCommandValid();
            _mockedUserRepository.Setup(x => x.Add(It.IsAny<UserModel>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}