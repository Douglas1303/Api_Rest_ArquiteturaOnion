using Bogus;
using Poc.Domain.Commands.Events;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class AddEventCommandFaker
    {
        public static AddEventCommand GetCommandValid()
        {
            return new Faker<AddEventCommand>("pt_BR")
                .CustomInstantiator(f => new AddEventCommand(
                    f.Lorem.Sentence(2),
                    f.Lorem.Sentence(4),
                    f.Date.Recent().AddDays(1),
                    f.Date.Recent().AddDays(4),
                    f.Random.Number(1, 10)
                    )).Generate(); 
        }
    }
}