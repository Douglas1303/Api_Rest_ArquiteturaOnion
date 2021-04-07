using AutoMapper;
using ExternalServices.Cep.Interface;
using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Test.ObjectsFakers.Dtos;
using Poc.Test.ObjectsFakers.ViewModel;
using System;
using System.Threading;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class SponsorApplicationTest
    {
        private readonly Mock<ISponsorRepository> _mockedSponsorRepository;
        private readonly Mock<IMediator> _mockedMediator;
        private readonly Mock<ILogModel> _mockedLogModel;
        private readonly IMapper _mapperFake;
        private readonly Mock<ICepService> _mockedCepService;
        private readonly Mock<IStringLocalizer<SponsorAppRsc>> _mockedLocalizer;
        private readonly SponsorApplication _sponsorApplication;

        public SponsorApplicationTest()
        {
            _mockedSponsorRepository = new Mock<ISponsorRepository>();
            _mockedMediator = new Mock<IMediator>();
            _mockedLogModel = new Mock<ILogModel>();
            _mockedCepService = new Mock<ICepService>();
            _mockedLocalizer = new Mock<IStringLocalizer<SponsorAppRsc>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);

            _sponsorApplication = new SponsorApplication(_mockedSponsorRepository.Object, _mockedMediator.Object, _mockedLogModel.Object,
                                                         _mapperFake, _mockedCepService.Object, _mockedLocalizer.Object);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryIsValid_ReturnShouldBeOkWithList()
        {
            //Arrange
            _mockedSponsorRepository.Setup(x => x.GetAll()).ReturnsAsync(SponsorDtoFaker.GetListDtoValid());

            //Act
            var requestResult = _sponsorApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedSponsorRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAllAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            _mockedSponsorRepository.Setup(x => x.GetAll()).Throws(new Exception());

            //Act
            var requestResult = _sponsorApplication.GetAllAsync();

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedSponsorRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void AddAsync_WhenRepositoryIsValid_ReturnShouldBeOK()
        {
            //Arrange
            IResult commandResult = new CommandResult();
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResult);
            var viewModel = AddSponsorViewModelFaker.GetViewModelValid();
            _mockedCepService.Setup(x => x.GetAddressAsync(viewModel.Cep)).ReturnsAsync(new CepModel { Cep = viewModel.Cep });
            _mockedSponsorRepository.Setup(x => x.AddAsync(It.IsAny<SponsorDto>())).Verifiable();

            //Act
            var requestResult = _sponsorApplication.AddAsync(viewModel);

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
            var viewModel = AddSponsorViewModelFaker.GetViewModelValid();
            _mockedCepService.Setup(x => x.GetAddressAsync(viewModel.Cep)).ReturnsAsync(new CepModel { Cep = viewModel.Cep });
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var requestResult = _sponsorApplication.AddAsync(viewModel);

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
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResult);
            _mockedSponsorRepository.Setup(x => x.RemoveAsync(1)).ReturnsAsync(string.Empty);

            //Act
            var requestResult = _sponsorApplication.RemoveAsync(1);

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
            _mockedMediator.Setup(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            //Act
            var requestResult = _sponsorApplication.RemoveAsync(1);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);
        }
    }
}