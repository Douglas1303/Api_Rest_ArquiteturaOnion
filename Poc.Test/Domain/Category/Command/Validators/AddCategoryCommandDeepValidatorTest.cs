using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Categories;
using Poc.Domain.Commands.Categories.Validators;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Xunit;

namespace Poc.Test.Domain.Category.Command.Validators
{
    public class AddCategoryCommandDeepValidatorTest
    {
        private readonly Mock<ICategoryRepository> _mockedCategoryRepository;
        private readonly AddCategoryCommandDeepValidator Validator;

        public AddCategoryCommandDeepValidatorTest()
        {
            _mockedCategoryRepository = new Mock<ICategoryRepository>();
            var mockedLocalizer = new Mock<IStringLocalizer<AddCategoryRsc>>();

            Validator = new AddCategoryCommandDeepValidator(mockedLocalizer.Object, _mockedCategoryRepository.Object); 
        }

        [Fact]
        public void Description_WhenDescriptionNotExist_ReturnShouldBeOk()
        {
            //Arrange
            var cmd = new AddCategoryCommand("Curso");
            _mockedCategoryRepository.Setup(x => x.DescriptionExists(It.IsAny<string>())).Returns(true);

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.True(result.IsValid); 
        }

        [Fact]
        public void Description_WhenDescriptionAlreadyExist_ReturnShouldBeOk()
        {
            //Arrange
            var cmd = new AddCategoryCommand("Curso");
            _mockedCategoryRepository.Setup(x => x.DescriptionExists(It.IsAny<string>())).Returns(false);

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}