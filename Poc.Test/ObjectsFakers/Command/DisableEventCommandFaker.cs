using Bogus;
using Poc.Domain.Commands.Events;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class DisableEventCommandFaker
    {
        public static DisableEventCommand GetCommandValid()
        {
            return new Faker<DisableEventCommand>("pt_BR")
                .CustomInstantiator(f => new DisableEventCommand(
                    f.Random.Number(1, 200)
                    )).Generate(); 
        }
    }
}