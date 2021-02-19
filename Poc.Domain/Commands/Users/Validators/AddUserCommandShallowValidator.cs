using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Resources;
using Poc.Domain.ValueObjects;

namespace Poc.Domain.Commands.Users.Validators
{
    public class AddUserCommandShallowValidator : BaseValidator<AddUserCommand, AddUserRsc>, IShallowValidator<AddUserCommand>
    {
        private const string CpfInvalid = "CpfInvalid";
        private const string CpfEmptyError = "CpfEmptyError";
        private const string LengthCpfInvalid = "LengthCpfInvalid";

        public AddUserCommandShallowValidator(IStringLocalizer<AddUserRsc> localizer) : base(localizer)
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage(x => GetMessage(CpfEmptyError))
                .WithErrorCode(CpfEmptyError);

            RuleFor(x => x.Cpf.Length).Equal(CpfVo.LengthCpf)
                .WithMessage(x => GetMessage(LengthCpfInvalid))
                .WithErrorCode(LengthCpfInvalid);

            RuleFor(x => CpfVo.IsValid(x.Cpf))
                .Equal(true)
                .WithMessage(x => GetMessage(CpfInvalid))
                .WithErrorCode(CpfInvalid); 
                
                    
                

            ////RuleFor(x => x.Cpf.Length).Equals(CpfVo.LengthCpf)
            ////    .WithMessage(x => GetMessage(LengthCpfInvalid))
            ////    .WithErrorCode(LengthCpfInvalid); 
            ////RuleFor(x => CpfVo.IsValid(x.Cpf))
            ////    .Equals(true)
            ////    .WithMessage(x => GetMessage(CpfInvalid))
            ////    .WithErrorCode(CpfInvalid);

            //RuleFor(c => c.Cpf)
            //  .NotEmpty()
            // .WithMessage(x => GetMessage(LengthCpfInvalid))
            // .WithErrorCode(LengthCpfInvalid);
        }
    }
}