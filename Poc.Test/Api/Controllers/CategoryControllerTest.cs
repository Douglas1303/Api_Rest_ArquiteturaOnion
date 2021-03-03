using Infra.CrossCutting.Core.CQRS;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using Xunit;

namespace Poc.Test.Api.Controllers
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryApplication> _mockedCategoryApplication;
        private readonly CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _mockedCategoryApplication = new Mock<ICategoryApplication>();
            _categoryController = new CategoryController(_mockedCategoryApplication.Object);
        }

        [Fact]
        public void GetAll_WhenServiceReturnsItems_ReturnShouldBeOk()
        {
            //Arrange
            IResult result = new QueryResult(GetCategoryModelValid());

            _mockedCategoryApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _categoryController.GetAllAsync();
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
        public void GetAll_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            IResult result = new CommandResult();
            result.AddErrorMessage("Error");

            _mockedCategoryApplication.Setup(x => x.GetAllAsync()).ReturnsAsync(result);

            //Act
            var response = _categoryController.GetAllAsync();
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            ////Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Null(content.Data);
            Assert.Equal(StatusResult.Error, content.Status);
        }

        [Fact]
        public void AddCategory_WhenServiceIsValid_ReturnShouldBeOk()
        {
            IResult commandResult = new CommandResult();

            //Arrange
            _mockedCategoryApplication.Setup(x => x.AddCategory(It.IsAny<AddCategoryViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _categoryController.AddCategoryAsync(GetAddCategoryViewModel());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Ok, content.Status);
        }

        [Fact]
        public void AddCategory_WhenServiceIsInvalid_ReturnShouldBeError()
        {
            IResult commandResult = new CommandResult();
            commandResult.AddErrorMessage("Error");
            //Arrange
            _mockedCategoryApplication.Setup(x => x.AddCategory(It.IsAny<AddCategoryViewModel>())).ReturnsAsync(commandResult);

            //Act
            var response = _categoryController.AddCategoryAsync(It.IsAny<AddCategoryViewModel>());
            var objectResult = response.Result as OkObjectResult;
            var content = objectResult.Value as IResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(objectResult);
            Assert.NotEmpty(content.Messages);
            Assert.Equal(StatusResult.Error, content.Status);
        }

        private CategoryModel GetCategoryModelValid()
        {
            return new CategoryModel("Curso programação");
        }

        private AddCategoryViewModel GetAddCategoryViewModel()
        {
            return new AddCategoryViewModel
            {
                Descricao = "Evento para programadores"
            };
        }
    }
}