using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class AddUserEventViewModelFaker 
    {
        public static AddUserEventViewModel GetViewModelValid()
        {
            return new Faker<AddUserEventViewModel>("pt_BR")
                 .RuleFor(x => x.EventoId, f => f.Random.Number(1, 99999))
                 .RuleFor(x => x.UsuarioId, f => f.Random.Number(1, 99999))
                 .Generate(); 
        }
    }
}