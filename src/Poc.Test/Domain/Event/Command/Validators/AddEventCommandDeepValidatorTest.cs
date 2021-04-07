using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Events;
using Poc.Domain.Commands.Events.Validators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using System;
using Xunit;

namespace Poc.Test.Domain.Event.Command.Validators
{
    public class AddEventCommandDeepValidatorTest
    {
        private readonly Mock<IEventRepository> _mockedEventRepository;
        private readonly AddEventCommandDeepValidator Validator;

        public AddEventCommandDeepValidatorTest()
        {
            _mockedEventRepository = new Mock<IEventRepository>();
            var mockedLocalizer = new Mock<IStringLocalizer<AddEventRsc>>();

            Validator = new AddEventCommandDeepValidator(mockedLocalizer.Object, _mockedEventRepository.Object); 
        }

        [Fact]
        public void CheckEventExists_WhenTitleNotExist_ReturnShouldBeOk()
        {
            //Arrange 
            var cmd = AddEventCommandFaker.GetCommandValid(); 
            _mockedEventRepository.Setup(x => x.TitleExists(It.IsAny<string>())).Returns(true);

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CheckEventExists_WhenTitleExist_ReturnShouldBeError()
        {
            //Arrange 
            var cmd = AddEventCommandFaker.GetCommandValid();
            _mockedEventRepository.Setup(x => x.TitleExists(It.IsAny<string>())).Returns(false);

            //Act
            ValidationResult result = Validator.Validate(cmd);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}