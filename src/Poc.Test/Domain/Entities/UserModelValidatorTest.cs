using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Domain.Entities;
using Poc.Domain.Entities.Validations;
using System;
using Xunit;

namespace Poc.Test.Domain.Entities
{
    public class UserModelValidatorTest
    {
        private readonly Faker _faker;
        public UserModelValidatorTest()
        {
            _faker = new Faker(); 
        }
        [Fact]
        public void UserModel_WhenModelIsValid_ReturnShouldBeOk()
        {
            //Arrange
            var model = GetValidUserModel();
            UserModelValidator validator = new UserModelValidator();

            //Act
            var validation = validator.Validate(model);

            //Assert
            Assert.True(validation.IsValid);
        }

        [Fact]
        public void UserModel_WhenModelIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            var model = GetInvalidUserModel();
            UserModelValidator validator = new UserModelValidator();

            //Act
            var validation = validator.Validate(model);

            //Assert
            Assert.False(validation.IsValid);
        }

        private UserModel GetValidUserModel()
        {
            return new UserModel(_faker.Random.Guid(), _faker.Person.FirstName, _faker.Person.Cpf().Replace("-", "").Replace(".", ""), DateTime.Parse("10/01/1996"), "teste@gmail.com");
        }

        private UserModel GetInvalidUserModel()
        {
            return new UserModel(_faker.Random.Guid(), string.Empty, "292453510763231313", DateTime.Parse("10/01/1996"), "test.com.br");
        }
    }
}