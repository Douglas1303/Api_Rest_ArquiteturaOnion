using Bogus;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Commands.Sponsor.Validators;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using Xunit;

namespace Poc.Test.Domain.Sponsor.Command.Validators
{
    public class AddSponsorCommandShallowValidatorTest : AddSponsorCommandFaker
    {
        private readonly AddSponsorCommandShallowValidator Validator;

        public AddSponsorCommandShallowValidatorTest()
        {
            var mockLocalizer = new Mock<IStringLocalizer<AddSponsorRsc>>();
            Validator = new AddSponsorCommandShallowValidator(mockLocalizer.Object);
        }

        [Fact]
        public void Validate_WhenCommandIsValid_ReturnShouldBeOk()
        {
            var cmd = GetAddSponsorCommand();

            FluentValidation.Results.ValidationResult result = Validator.Validate(cmd);

            Assert.True(result.IsValid);
        }
    }
}