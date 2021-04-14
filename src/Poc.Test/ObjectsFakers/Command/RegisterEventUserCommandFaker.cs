using Bogus;
using Poc.Domain.Commands.Events;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class RegisterEventUserCommandFaker
    {
        public static RegisterEventUserCommand GetCommandValid()
        {
            return new Faker<RegisterEventUserCommand>("pt_BR")
                .CustomInstantiator(f => new RegisterEventUserCommand(
                    f.Random.Guid().ToString(),
                    f.Random.Number(1, 100)
                    )).Generate();
        }
    }
}