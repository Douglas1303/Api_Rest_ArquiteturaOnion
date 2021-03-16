using Poc.Domain.Entities;
using Poc.Domain.Entities.Validations;
using Xunit;

namespace Poc.Test.Domain.Entities
{
    public class CategoryModelValidatorTest
    {
        [Fact]
        public void CategoryModel_WhenModelIsValid_ReturnShouldBeOk()
        {
            //Arrange
            var model = GetValidCategoryModel();
            CategoryModelValidator validator = new CategoryModelValidator();

            //Act
            var validation = validator.Validate(model);

            //Assert
            Assert.True(validation.IsValid); 

        }

        [Fact]
        public void CategoryModel_WhenModelIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            var model = GetInvalidCategoryModel();
            CategoryModelValidator validator = new CategoryModelValidator();

            //Act
            var validation = validator.Validate(model);

            //Assert
            Assert.False(validation.IsValid);
        }

        private CategoryModel GetValidCategoryModel()
        {
            return new CategoryModel("Bootcamp");
        }

        private CategoryModel GetInvalidCategoryModel()
        {
            return new CategoryModel(string.Empty);
        }
    }
}