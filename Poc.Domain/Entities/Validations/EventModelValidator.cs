using FluentValidation;

namespace Poc.Domain.Entities.Validations
{
    public class EventModelValidator : AbstractValidator<EventModel>
    {
        public EventModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().NotNull().MinimumLength(2).MaximumLength(300);
            RuleFor(x => x.Descricao).NotEmpty().NotNull().MinimumLength(2).MaximumLength(300);
            RuleFor(x => x.DataInicio).NotNull();
            RuleFor(x => x.DataFim).NotNull();
            RuleFor(x => x.Ativo).NotNull();
            RuleFor(x => x.DataCadastro).NotNull();
        }
    }
}