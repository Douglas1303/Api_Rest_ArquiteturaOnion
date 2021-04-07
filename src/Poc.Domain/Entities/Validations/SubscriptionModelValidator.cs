using FluentValidation;

namespace Poc.Domain.Entities.Validations
{
    public class SubscriptionModelValidator : AbstractValidator<SubscriptionModel>
    {
        public SubscriptionModelValidator()
        {
            RuleFor(x => x.UsuarioId).NotNull().WithMessage("UsuarioId tem que ser preenchido.").GreaterThan(0);
            RuleFor(x => x.EventoId).NotNull().WithMessage("EventoId tem que ser preenchido.").GreaterThan(0);
            RuleFor(x => x.DataCadastro).NotNull().WithMessage("Data de cadastro tem que ser preenchida."); 
        }
    }
}