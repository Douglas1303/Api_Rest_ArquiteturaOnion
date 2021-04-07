using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Resources;

namespace Poc.Domain.Commands.Events.Validators
{
    internal class AddEventCommandShallowValidator : BaseValidator<AddEventCommand, AddEventRsc>, IShallowValidator<AddEventCommand>
    {
        private const string TitleIsEmptyErrror = "TitleIsEmptyErrror";
        private const string DescriptionIsEmptyErrror = "DescriptionIsEmptyErrror";
        private const string InitialDateIsEmptyErrror = "InitialDateIsEmptyErrror";
        private const string EndDateIsEmptyErrror = "EndDateIsEmptyErrror";
        private const string CategoryIdNullErrror = "CategoryIdNullErrror";

        public AddEventCommandShallowValidator(IStringLocalizer<AddEventRsc> localizer) : base(localizer)
        {
            RuleFor(c => c.Titulo)
               .NotEmpty()
              .WithMessage(x => GetMessage(TitleIsEmptyErrror))
              .WithErrorCode(TitleIsEmptyErrror);

            RuleFor(c => c.Descricao)
               .NotEmpty()
              .WithMessage(x => GetMessage(DescriptionIsEmptyErrror))
              .WithErrorCode(DescriptionIsEmptyErrror);

            RuleFor(c => c.DataInicio)
              .NotEmpty()
             .WithMessage(x => GetMessage(InitialDateIsEmptyErrror))
             .WithErrorCode(InitialDateIsEmptyErrror);

            RuleFor(c => c.DataFim)
             .NotEmpty()
            .WithMessage(x => GetMessage(EndDateIsEmptyErrror))
            .WithErrorCode(EndDateIsEmptyErrror);

            RuleFor(c => c.CategoriaId)
                .NotNull()
                .WithMessage(x => GetMessage(CategoryIdNullErrror))
                .WithErrorCode(CategoryIdNullErrror);
        }
    }
}