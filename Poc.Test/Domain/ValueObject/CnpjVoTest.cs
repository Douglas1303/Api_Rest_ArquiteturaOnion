using Poc.Domain.ValueObjects;
using Xunit;

namespace Poc.Test.Domain.ValueObject
{
    public class CnpjVoTest
    {
        [Theory]
        [InlineData("25015502000117")]
        [InlineData("78.070.722/0001-00")]
        public void Validate_WHenCnpjIsValid_ReturnShouldBeValid(string cpf)
        {
            var isValid = CnpjVo.IsValid(cpf);

            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("12131325015502000117")]
        [InlineData("1378.070.722/0001-00131")]
        public void Validate_WHenCnpjIsInvalid_ReturnShouldBeInvalid(string cpf)
        {
            var isValid = CnpjVo.IsValid(cpf);

            //Assert
            Assert.False(isValid);
        }
    }
}