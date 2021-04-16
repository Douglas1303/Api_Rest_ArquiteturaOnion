using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.CommandHandlers.Sponsor;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Domain.Sponsor.CommandHandler
{
    public class AddSponsorCommandHandlerTest
    {
        private readonly Mock<ISponsorRepository> _mockedSponsorRepository;
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IStringLocalizer<AddSponsorCommandHandlerRsc>> _mockedLocalizer;
        private readonly AddSponsorCommandHandler _commandHandler;

        public AddSponsorCommandHandlerTest()
        {
            _mockedSponsorRepository = new Mock<ISponsorRepository>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedLocalizer = new Mock<IStringLocalizer<AddSponsorCommandHandlerRsc>>();

            _commandHandler = new AddSponsorCommandHandler(_mockedSponsorRepository.Object, _mockedUnitOfWork.Object, _mockedLocalizer.Object); 
        }

        [Fact]
        public void Handler_WhenCommandIsValid_ReturnShoulBeOk()
        {
            //Arrange
            var command = AddSponsorCommandFaker.GetCommandValid();
            _mockedSponsorRepository.Setup(x => x.AddAsync(It.IsAny<SponsorDto>())).ReturnsAsync(1);

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
            var command = AddSponsorCommandFaker.GetCommandValid();
            _mockedSponsorRepository.Setup(x => x.AddAsync(It.IsAny<SponsorDto>())).Throws(new Exception());

            //Act
            var response = _commandHandler.Handle(command, new CancellationToken()).Result;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Status.ToString(), StatusResult.Error.ToString());
        }
    }
}