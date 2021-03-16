using Poc.Domain.Entities;
using Poc.Domain.Entities.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poc.Test.Domain.Entities
{
    public class EventModelValidatorTest
    {
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
            return new EventModel("Live", "Live de programação", DateTime.Now, DateTime.Now.AddDays(2), 1); 
        }

        private EventModel GetInvalidEventModel()
        {
            return new EventModel(string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(2), 1);
        }
    }
}
