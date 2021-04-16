using Bogus;
using Poc.Domain.Commands.Events;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class UpdateEventCommandFaker
    {
        public static UpdateEventCommand GetCommandValid()
        {
            return new Faker<UpdateEventCommand>("pt_BR")
                .CustomInstantiator(f => new UpdateEventCommand(
                        f.Random.Number(1, 100),
                        f.Lorem.Sentence(2),
                        f.Lorem.Sentence(4),
                        f.Date.Recent().AddDays(1),
                        f.Date.Recent().AddDays(4),
                        f.IndexFaker == 0 ? false : true,
                        f.Random.Number(1, 10)
                    )).Generate();
        }
    }
}