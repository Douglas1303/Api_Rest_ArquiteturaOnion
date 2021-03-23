using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.FIle;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.File.CommandHandler
{
    public class AddFileCommandHandlerTest
    {
        private readonly Mock<IFileRepository> _mockedFileRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<AddFileCommandHandlerRsc>> _mockedLocalizer;
        private readonly AddFileCommandHandler _commandHandler;

        public AddFileCommandHandlerTest()
        {
            _mockedFileRepository = new Mock<IFileRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<AddFileCommandHandlerRsc>>();

            _commandHandler = new AddFileCommandHandler(_mockedFileRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = AddFileCommandFaker.GetCommandValid();
            _mockedFileRepository.Setup(x => x.AddAsync(It.IsAny<FileDto>())).Verifiable();

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
            var command = AddFileCommandFaker.GetCommandValid();
            _mockedFileRepository.Setup(x => x.AddAsync(It.IsAny<FileDto>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}