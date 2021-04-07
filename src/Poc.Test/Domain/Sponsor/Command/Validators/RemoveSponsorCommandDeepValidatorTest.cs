using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Commands.Sponsor.Validators;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using Xunit;

namespace Poc.Test.Domain.Sponsor.Command.Validators
{
    public class RemoveSponsorCommandDeepValidatorTest
    {
        private readonly Mock<ISponsorRepository> _mockedSponsorRepository;
        private readonly RemoveSponsorCommandDeepValidator Validator;

        public RemoveSponsorCommandDeepValidatorTest()
        {
            _mockedSponsorRepository = new Mock<ISponsorRepository>(); 
            var mockLocalizer = new Mock<IStringLocalizer<RemoveSponsorRsc>>();

            Validator = new RemoveSponsorCommandDeepValidator(_mockedSponsorRepository.Object, mockLocalizer.Object);
        }

        [Fact]
        public void SponsorExists_WhenSponsorExists_ReturnShouldBeOk()
        {
            _mockedSponsorRepository.Setup(x => x.SponsorExists(It.IsAny<int>())).ReturnsAsync(new SponsorDto());

            var cmd = RemoveSponsorCommandFaker.GetCommandValid(); 

            ValidationResult result = Validator.Validate(cmd);

            Assert.True(result.IsValid); 
        }
    }
}