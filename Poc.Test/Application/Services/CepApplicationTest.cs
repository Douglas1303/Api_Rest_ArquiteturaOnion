using AutoMapper;
using ExternalServices.Cep.Interface;
using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Application.AutoMapper;
using Poc.Application.Service;
using Poc.Domain.Resources.Application;
using System;
using Xunit;

namespace Poc.Test.Application.Services
{
    public class CepApplicationTest
    {
        private readonly Mock<ICepService> _mockedCepService;
        private readonly Mock<IStringLocalizer<CepAppRsc>> _mockedLocalizer;
        private readonly IMapper _mapperFake;
        private readonly CepApplication _cepApplication;

        public CepApplicationTest()
        {
            _mockedCepService = new Mock<ICepService>();
            _mockedLocalizer = new Mock<IStringLocalizer<CepAppRsc>>(); 

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });

            _mapperFake = new Mapper(config);  

            _cepApplication = new CepApplication(_mockedCepService.Object, _mapperFake, _mockedLocalizer.Object);
        }

        [Fact]
        public void GetCepAsync_WhenRepositoryIsValid_ReturnShouldBeOk()
        {
            //Arrange 
            _mockedCepService.Setup(x => x.GetAddressAsync("08151150")).ReturnsAsync(GetCepModel());

            //Act
            var requestResult = _cepApplication.GetCepAsync("08151150");

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
            _mockedCepService.Setup(x => x.GetAddressAsync("08151150")).Throws(new Exception());

            //Act
            var requestResult = _cepApplication.GetCepAsync("08151150");

            //Assert
            Assert.NotNull(requestResult);
            Assert.NotEmpty(requestResult.Result.Messages);
            Assert.Null(requestResult.Result.Data);
            Assert.Equal(StatusResult.Error, requestResult.Result.Status);

            _mockedCepService.Verify(x => x.GetAddressAsync(It.IsAny<string>()), Times.Once);
        }

        private CepModel GetCepModel()
        {
            return new CepModel
            {
                Cep = "08151150",
                Logradouro = "Rua da Paz, 5020",
                Complemento = "Casa 2",
                Localidade = "Rio de Janeiro",
                Bairro = "Jd. das flores",
                UF = "RJ",
                DDD = "17"
            }; 
        }
    }
}