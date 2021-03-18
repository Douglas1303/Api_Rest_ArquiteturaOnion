using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class AddUserEventViewModelFaker : Faker<AddUserEventViewModel>
    {
        public AddUserEventViewModelFaker()
        {
            var eventId = new Faker().Random.Number(1, 99999);
            var userId = new Faker().Random.Number(1, 99999);

            RuleFor(x => x.EventoId, f => eventId);
            RuleFor(x => x.UsuarioId, f => userId);
        }
    }
}