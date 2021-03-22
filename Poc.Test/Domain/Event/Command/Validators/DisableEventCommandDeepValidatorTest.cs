using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Events;
using Poc.Domain.Commands.Events.Validators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Xunit;

namespace Poc.Test.Domain.Event.Command.Validators
{
    public class DisableEventCommandDeepValidatorTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly DisableEventCommandDeepValidator Validator;

        public DisableEventCommandDeepValidatorTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            var mockedLocalizer = new Mock<IStringLocalizer<DisableEventRsc>>();

            Validator = new DisableEventCommandDeepValidator(mockedLocalizer.Object, _mockedEventRepository.Object);
        }

        [Fact]
        public void EventIdExists_WhenEventIdNotExist_ReturnShouldBeOk()
        {
            //Arrange
            _mockedEventRepository.Setup(x => x.EventIdExists(It.IsAny<int>())).Returns(true);
            _mockedEventRepository.Setup(x => x.StatusIsFalse(It.IsAny<int>())).Returns(true);

            var cmd = new DisableEventCommand(It.IsAny<int>());

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EventIdExists_WhenEventIdExist_ReturnShouldBeError()
        {
            //Arrange
            var cmd = new DisableEventCommand(1);
            _mockedEventRepository.Setup(x => x.EventIdExists(1)).Returns(false);

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}