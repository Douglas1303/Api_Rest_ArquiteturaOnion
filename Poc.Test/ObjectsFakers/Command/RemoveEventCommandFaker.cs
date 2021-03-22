using Bogus;
using Poc.Domain.Commands.Events;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class RemoveEventCommandFaker
    {
        public static RemoveEventCommand GetCommandValid()
        {
            return new Faker<RemoveEventCommand>("pt_BR")
                .CustomInstantiator(f => new RemoveEventCommand(
                    f.Random.Number(1, 100)
                    )).Generate(); 
        }
    }
}