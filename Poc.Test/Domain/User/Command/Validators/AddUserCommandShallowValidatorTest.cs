using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Users.Validators;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poc.Test.Domain.User.Command.Validators
{
    public class AddUserCommandShallowValidatorTest
    {
        private readonly AddUserCommandShallowValidator Validator;

        public AddUserCommandShallowValidatorTest()
        {
            var mockLocalizer = new Mock<IStringLocalizer<AddUserRsc>>();

            Validator = new AddUserCommandShallowValidator(mockLocalizer.Object); 
        }

        [Fact]
        public void Validate()
        {
            var cmd = AddUserCommandFaker.GetCommandValid();

            ValidationResult result = Validator.Validate(cmd);

            Assert.True(result.IsValid); 
        }

        [Fact]
        public void Validate_WhenCpfIsEmpty_ReturnShouldBeError()
        {
            var cmd = AddUserCommandFaker.GetCommandCpfEmpty();

            ValidationResult result = Validator.Validate(cmd);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_WhenCpfFormatIvalid_ReturnShouldBeError()
        {
            var cmd = AddUserCommandFaker.GetCommandCpfFormatInvalid();

            ValidationResult result = Validator.Validate(cmd);

            Assert.False(result.IsValid);
        }
    }
}
