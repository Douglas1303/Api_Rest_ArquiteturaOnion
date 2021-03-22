using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Test.ObjectsFakers.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class SponsorControllerTest : AddSponsorViewModelFaker
    {
        private readonly Mock<ISponsorApplication> _mockedSponsorApplication;
        private readonly SponsorController _sponsorController;
        private readonly List<SponsorViewModel> _listSponsorViewModel; 

        public SponsorControllerTest()
        {
            _listSponsorViewModel = new SponsorViewModelFaker().Generate(5); 

            _mockedSponsorApplication = new Mock<ISponsorApplication>();

            _sponsorController = new SponsorController(_mockedSponsorApplication.Object);
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(_listSponsorViewModel);

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
            var response = _sponsorController.AddAsync(GetAddSponsorViewModelFaker());
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
    }
}