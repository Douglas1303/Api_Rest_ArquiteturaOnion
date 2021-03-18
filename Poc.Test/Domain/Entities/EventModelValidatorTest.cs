using Bogus;
using Poc.Domain.Entities;
using Poc.Domain.Entities.Validations;
using System;
using Xunit;

namespace Poc.Test.Domain.Entities
{
    public class EventModelValidatorTest
    {
        private readonly Faker _faker;

        public EventModelValidatorTest()
        {
            _faker = new Faker(); 
        }

        [Fact]
        public void EventModel_WhenModelIsValid_ReturnShouldBeOk()
        {
            //Arrange
            var model = GetValidEventModel();
            EventModelValidator Validator = new EventModelValidator();

            //Act
            var Validation = Validator.Validate(model);

            //Assert
            Assert.True(Validation.IsValid);
        }

        [Fact]
        public void EventModel_WhenModelIsInvalid_ReturnShouldBeError()
        {
            //Arrange
            var model = GetInvalidEventModel();
            EventModelValidator Validator = new EventModelValidator();

            //Act
            var Validation = Validator.Validate(model);

            //Assert
            Assert.False(Validation.IsValid);
        }

        private EventModel GetValidEventModel()
        {
            return new EventModel(_faker.Lorem.Sentence(5), _faker.Lorem.Sentence(20), DateTime.Now, DateTime.Now.AddDays(2), _faker.Random.Number(1, 999999));
        }

        private EventModel GetInvalidEventModel()
        {
            return new EventModel(string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(2), 1);
        }
    }
}