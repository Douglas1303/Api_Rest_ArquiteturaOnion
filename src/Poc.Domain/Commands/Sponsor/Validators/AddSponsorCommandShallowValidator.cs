using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Enum;
using Poc.Domain.Resources;
using Poc.Domain.ValueObjects;
using System;

namespace Poc.Domain.Commands.Sponsor.Validators
{
    internal class AddSponsorCommandShallowValidator : BaseValidator<AddSponsorCommand, AddSponsorRsc>, IShallowValidator<AddSponsorCommand>
    {
        private const string CepInvalidError = "CepInvalidError"; 
        private const string DocumentEmptyError = "DocumentEmptyError"; 
        private const string DocumentLengthError = "DocumentLengthError"; 
        private const string DocumentInvalidError = "DocumentInvalidError"; 
        private const string DocumentNotExistsError = "DocumentNotExistsError"; 
        public AddSponsorCommandShallowValidator(IStringLocalizer<AddSponsorRsc> localizer) : base(localizer)
        {
            RuleFor(c => c.Cep)
              .NotNull()
             .WithMessage(x => GetMessage(CepInvalidError))
             .WithErrorCode(CepInvalidError);

            RuleFor(x => x.Documento)
               .NotEmpty()
               .WithMessage(x => GetMessage(DocumentEmptyError))
               .WithErrorCode(DocumentEmptyError);

            When(x => x.TipoPatrocinador == ETipoPatrocinador.PessoaFisica, () =>
            {
                RuleFor(x => x.Documento.Length).Equal(CpfVo.LengthCpf)
                    .WithMessage(x => GetMessage(DocumentLengthError))
                    .WithErrorCode(DocumentLengthError);

                    RuleFor(x => CpfVo.IsValid(x.Documento))
                        .Equal(true)
                        .WithMessage(x => GetMessage(DocumentInvalidError))
                        .WithErrorCode(DocumentInvalidError);
            });

            When(x => x.TipoPatrocinador == ETipoPatrocinador.PessoaJuridica, () =>
            {
                RuleFor(x => x.Documento.Length).Equal(CnpjVo.LengthCnpj)
                    .WithMessage(x => GetMessage(DocumentLengthError))
                    .WithErrorCode(DocumentLengthError);

                RuleFor(x => CnpjVo.IsValid(x.Documento))
                    .Equal(true)
                    .WithMessage(x => GetMessage(DocumentInvalidError))
                    .WithErrorCode(DocumentInvalidError);
            });

            When(x => x.TipoPatrocinador == ETipoPatrocinador.NaoExiste, () =>
            {
                RuleFor(X => X.Documento)
                .Empty()
                .WithMessage(x => GetMessage(DocumentNotExistsError))
                .WithErrorCode(DocumentNotExistsError); 
            });
        }
    }
}