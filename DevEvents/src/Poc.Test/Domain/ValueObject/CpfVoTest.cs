using Poc.Domain.ValueObjects;
using Xunit;

namespace Poc.Test.Domain.ValueObject
{
    public class CpfVoTest
    {
        [Theory]
        [InlineData("80023719095")]
        [InlineData("432.540.040-00")]
        public void Validate_WHenCpfIsValid_ReturnShouldBeValid(string cpf)
        {
            var isValid = CpfVo.IsValid(cpf);

            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("80023719091215")]
        [InlineData("432.540.040-0013131")]
        public void Validate_WHenCpfIsInvalid_ReturnShouldBeInvalid(string cpf)
        {
            var isValid = CpfVo.IsValid(cpf);

            //Assert
            Assert.False(isValid);
        }
    }
}