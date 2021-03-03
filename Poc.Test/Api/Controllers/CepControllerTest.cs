using ExternalServices.Cep.Model;
using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class CepControllerTest
    {
        private readonly Mock<ICepApplication> _mockedCepApplication;
        private readonly CepController _cepController;

        public CepControllerTest()
        {
            _mockedCepApplication = new Mock<ICepApplication>();
            _cepController = new CepController(_mockedCepApplication.Object);
        }

        [Fact]
        public void GetByCepAsync_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetCepModelValid());

            _mockedCepApplication.Setup(x => x.GetCepAsync("69086692")).ReturnsAsync(result);

            //Act
            var response = _cepController.GetByCepAsync("69086692");
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            ////Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.NotNull(content.Data);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void GetByCepAsync_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            IResult result = new CommandResult();
            result.AddErrorMessage("Error");

            _mockedCepApplication.Setup(x => x.GetCepAsync(It.IsAny<string>())).ReturnsAsync(result);

            //Act
            var response = _cepController.GetByCepAsync(It.IsAny<string>());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            ////Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Null(content.Data);
            Assert.Equal(StatusResult.Error, content.Status);
        }

        private CepModel GetCepModelValid()
        {
            return new CepModel
            {
                Cep = "69086692",
                Logradouro = "Rua Garça Azul",
                Complemento = "Casa 2",
                Localidade = "Manaus",
                Bairro = "Jd. Paulo Mauro",
                UF = "AM",
                DDD = "12"
            };
        }
    }
}