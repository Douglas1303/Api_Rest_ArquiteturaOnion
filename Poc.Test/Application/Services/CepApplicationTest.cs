using AutoMapper;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Resources.Application;
using Poc.Test.ObjectsFakers.Entities;
using System;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class CepApplicationTest
    {
        private readonly Mock<ICepService> _mockedCepService;
        private readonly Mock<IStringLocalizer<CepAppRsc>> _mockedLocalizer;
        private readonly IMapper _mapperFake;
        private readonly Mock<ILogModel> _mockedLog;
        private readonly CepApplication _cepApplication;

        public CepApplicationTest()
        {
            _mockedCepService = new Mock<ICepService>();
            _mockedLocalizer = new Mock<IStringLocalizer<CepAppRsc>>();
            _mockedLog = new Mock<ILogModel>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);

            _cepApplication = new CepApplication(_mockedCepService.Object, _mapperFake, _mockedLocalizer.Object, _mockedLog.Object);
        }

        [Fact]
        public void GetCepAsync_WhenRepositoryIsValid_ReturnShouldBeOk()
        {
            //Arrange
            var model = CepModelFaker.GetModelValid(); 
            _mockedCepService.Setup(x => x.GetAddressAsync(model.Cep)).ReturnsAsync(model);

            //Act
            var requestResult = _cepApplication.GetCepAsync(model.Cep);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.NotNull(requestResult.Result.Data);
            Assert.Equal(StatusResult.Ok, requestResult.Result.Status);

            _mockedCepService.Verify(x => x.GetAddressAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetCepAsync_WhenRepositoryReturnException_ReturnShouldBeError()
        {
            //Arrange
            var model = CepModelFaker.GetModelValid();
            _mockedCepService.Setup(x => x.GetAddressAsync(model.Cep)).Throws(new Exception());

            //Act
            var requestResult = _cepApplication.GetCepAsync(model.Cep);

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedCepService.Verify(x => x.GetAddressAsync(It.IsAny<string>()), Times.Once);
        }
    }
}