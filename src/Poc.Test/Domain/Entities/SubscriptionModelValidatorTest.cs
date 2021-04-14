using Poc.Domain.Entities;
using Poc.Domain.Entities.Validations;
using System;
using Xunit;

namespace Poc.Test.Domain.Entities
{
    public class SubscriptionModelValidatorTest
    {
        [Fact]
        public void SubscriptionModel_WhenModelIsValid_ReturnshouldBeOk()
        {
            //Arrange
            var model = GetValidSubscriptionModel();
            SubscriptionModelValidator validator = new SubscriptionModelValidator();

            //Act
            var validation = validator.Validate(model);

            //Assert
            Assert.True(validation.IsValid); 
        }

        [Fact]
        public void SubscriptionModel_WhenModelIsInvalid_ReturnshouldBeError()
        {
            //Arrange
            var model = GetInvalidSubscriptionModel();
            SubscriptionModelValidator validator = new SubscriptionModelValidator();

            //act
            var validation = validator.Validate(model);

            //Assert
            Assert.False(validation.IsValid);
        }

        private SubscriptionModel GetValidSubscriptionModel()
        {
            return new SubscriptionModel(new Guid(),20);
        }

        private SubscriptionModel GetInvalidSubscriptionModel()
        {
            return new SubscriptionModel(new Guid(), -1);
        }
    }
}