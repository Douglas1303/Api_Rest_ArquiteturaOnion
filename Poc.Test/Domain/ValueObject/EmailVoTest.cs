using Poc.Domain.ValueObjects;
using Xunit;

namespace Poc.Test.Domain.ValueObject
{
    public class EmailVoTest
    {
        [Theory]
        [InlineData("test@gmail.com")]
        [InlineData("123@hotmail.com.br")]
        public void Validate_WHenEmailIsValid_ReturnShouldBeValid(string email)
        {
            //Arrange
            var phone = new EmailVo(email);

            //Act
            var isValid = phone.IsValid();

            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("testgmail.com")]
        [InlineData("123hotmail.com.br")]
        public void Validate_WHenEmailIsInvalid_ReturnShouldBeInvalid(string email)
        {
            //Arrange
            var phone = new EmailVo(email);

            //Act
            var isValid = phone.IsValid();

            //Assert
            Assert.False(isValid);
        }
    }
}