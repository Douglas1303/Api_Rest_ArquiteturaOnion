using FluentValidation;
using Poc.Domain.ValueObjects;
using System;

namespace Poc.Domain.Entities.Validations
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.NomeCompleto).NotNull().NotEmpty().MinimumLength(2).MaximumLength(300);
            RuleFor(x => x.Cpf.Length).Equal(CpfVo.LengthCpf).NotNull().NotEmpty();
            RuleFor(x => x.DataCadastro).NotNull().NotEmpty().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-130));
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(); 
        }
    }
}