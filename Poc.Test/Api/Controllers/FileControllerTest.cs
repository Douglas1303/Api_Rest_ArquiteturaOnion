using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class FileControllerTest
    {
        private readonly Mock<IFileApplication> _mockedFileApplication;
        private readonly FileController _fileController;

        public FileControllerTest()
        {
            _mockedFileApplication = new Mock<IFileApplication>();

            _fileController = new FileController(_mockedFileApplication.Object);
        }

        [Fact]
        public void Add_WHenServiceIsValid_ReturnShouldBeOK()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedFileApplication.Setup(x => x.AddAsync(It.IsAny<AddFileViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _fileController.Add(It.IsAny<AddFileViewModel>());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }
    }
}