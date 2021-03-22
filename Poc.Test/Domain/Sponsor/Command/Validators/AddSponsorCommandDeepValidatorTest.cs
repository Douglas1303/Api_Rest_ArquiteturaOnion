﻿using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.Extensions.Localization;
using Moq;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Commands.Sponsor.Validators;
using Poc.Domain.Enum;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using Poc.Test.ObjectsFakers.Command;
using Xunit;

namespace Poc.Test.Domain.Sponsor.Command.Validators
{
    public class AddSponsorCommandDeepValidatorTest : AddSponsorCommandFaker
    {
        private readonly Mock<ISponsorRepository> _mockedSponsorRepository;
        private readonly AddSponsorCommandDeepValidator Validator;
        private readonly Faker _faker;

        public AddSponsorCommandDeepValidatorTest()
        {
            _mockedSponsorRepository = new Mock<ISponsorRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<AddSponsorRsc>>();

            _faker = new Faker();

            Validator = new AddSponsorCommandDeepValidator(_mockedSponsorRepository.Object, mockLocalizer.Object);
        }

        [Fact]
        public void NameSponsorExists_WhenCommandIsValid_ReturnShouldBeOk()
        {
            _mockedSponsorRepository.Setup(x => x.NameSponsorExists(It.IsAny<string>())).Returns(true);

            var cmd = GetAddSponsorCommand();

            FluentValidation.Results.ValidationResult result = Validator.Validate(cmd);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void NameSponsorExists_WhenCommandIsInvalid_ReturnShouldBeError()
        {
            _mockedSponsorRepository.Setup(x => x.NameSponsorExists(It.IsAny<string>())).Returns(false);

            var cmd = GetAddSponsorCommand();

            FluentValidation.Results.ValidationResult result = Validator.Validate(cmd);

            Assert.False(result.IsValid);
        }
    }
}