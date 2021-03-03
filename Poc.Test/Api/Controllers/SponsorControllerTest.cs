using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class SponsorControllerTest
    {
        private readonly Mock<ISponsorApplication> _mockedSponsorApplication;
        private readonly SponsorController _sponsorController;

        public SponsorControllerTest()
        {
            _mockedSponsorApplication = new Mock<ISponsorApplication>();

            _sponsorController = new SponsorController(_mockedSponsorApplication.Object); 
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetSponsorViewModelValid());

            _mockedSponsorApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _sponsorController.GetAll();
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.NotNull(content.Data);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void AddAsync_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedSponsorApplication.Setup(x => x.AddAsync(It.IsAny<AddSponsorViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _sponsorController.AddAsync(AddSponsorViewModelValid());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void Remove_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedSponsorApplication.Setup(x => x.RemoveAsync(2)).ReturnsAsync(commandResult);

            //Act
            var response = _sponsorController.Remove(2);
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        private AddSponsorViewModel AddSponsorViewModelValid()
        {
            return new AddSponsorViewModel 
            {
                TipoPatrocinador = 1,
                NomePatrocinador = "Dev+",
                Documento = "89239547053",
                Cep = "58086080",
                Logradouro = "Rua Desembargador Arquimedes",
                Complemento = "",
                Bairro = "Jd. das Aves",
                Localidade = "João Pessoa",
                UF = "PB",
                DDD = 17
            };
        }

        private List<SponsorViewModel> GetSponsorViewModelValid()
        {
            return new List<SponsorViewModel>
            {
                new SponsorViewModel
                {
                    NomePatrocinador = "Dev+",
                    Documento = "89239547053",
                    Cep = "58086080",
                    Logradouro = "Rua Desembargador Arquimedes",
                    Complemento = "", 
                    Bairro = "Jd. das Aves",
                    Localidade = "João Pessoa",
                    UF = "PB",
                    DDD = 17
                },
                 new SponsorViewModel
                {
                    NomePatrocinador = "CasaTec",
                    Documento = "63583430093",
                    Cep = "23090420",
                    Logradouro = "Rua Alcântara Jasmim",
                    Complemento = "Bloco 5, Ap 12",
                    Bairro = "Rua 7",
                    Localidade = "Rio de Janeiro",
                    UF = "RJ",
                    DDD = 18
                }
            };
        }
    }
}