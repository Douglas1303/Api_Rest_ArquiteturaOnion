using Poc.Domain.Enum;
using Poc.Domain.ValueObjects;
using Xunit;

namespace Poc.Test.Domain.ValueObject
{
    public class PhoneVoTest
    {
        [Theory]
        [InlineData(EPhoneType.Comercial, "(41) 2117-7307")]
        [InlineData(EPhoneType.Residencial, "(11) 2478-1512")]
        [InlineData(EPhoneType.Celular, "(11) 98691-2702")]
        [InlineData(EPhoneType.WhatsApp, "(11) 97198-1163")]
        public void Validate_WHenPhoneIsValid_ReturnShouldBeValid(EPhoneType phoneType, string phoneNumber)
        {
            //Arrange
            var phone = new PhoneVo(phoneType, phoneNumber);

            //Act
            var isValid = phone.IsValid();

            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(EPhoneType.Comercial, "(041) 2117-7307")]
        [InlineData(EPhoneType.Residencial, "(011) 2478-1512")]
        [InlineData(EPhoneType.Celular, "(011) 8691-2702")]
        [InlineData(EPhoneType.WhatsApp, "(011) 7198-1163")]
        public void Validate_WhenPhoneIsInvalid_ReturnShouldBeInvalid(EPhoneType phoneType, string phoneNumber)
        {
            //Arrange
            var phone = new PhoneVo(phoneType, phoneNumber);

            //Act
            var isValid = phone.IsValid();

            //Assert
            Assert.False(isValid);
        }


        [Theory]
        [InlineData(0, "(041) 2117-7307")]
        public void Validate_WhenPhoneTypeIsFalse_ReturnShouldBeInvalid(int phoneType, string phoneNumber)
        {
            //Arrange
            var phone = new PhoneVo((EPhoneType)phoneType, phoneNumber);

            //Act
            var isValid = phone.IsValid();

            //Assert
            Assert.False(isValid);
        }
    }
}