using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Users;
using Poc.Domain.Commands.Users.Validators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using Xunit;

namespace Poc.Test.Domain.User.Command.Validators
{
    public class AddUserCommandDeepValidatorTest 
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly AddUserCommandDeepValidator Validator;

        public AddUserCommandDeepValidatorTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<AddUserRsc>>();

            Validator = new AddUserCommandDeepValidator(mockLocalizer.Object, _mockedUserRepository.Object);
        }

        [Fact]
        public void Validate_WhenCommandIsValid_ReturnShouldBeOk()
        {
            _mockedUserRepository.Setup(x => x.UserExists(It.IsAny<string>())).Returns(true);

            var cmd = AddUserCommandFaker.GetCommandValid();

            ValidationResult result = Validator.Validate(cmd);

            Assert.True(result.IsValid); 
        }

        [Fact]
        public void Validate_WhenCommandInvalid_ReturnShouldBeError()
        {
            _mockedUserRepository.Setup(x => x.UserExists(It.IsAny<string>())).Returns(false);

            var cmd = AddUserCommandFaker.GetCommandValid();

            ValidationResult result = Validator.Validate(cmd);

            Assert.False(result.IsValid);
        }
    }
}