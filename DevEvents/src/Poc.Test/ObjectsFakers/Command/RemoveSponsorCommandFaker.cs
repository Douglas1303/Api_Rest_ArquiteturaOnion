using Bogus;
using Poc.Domain.Commands.Sponsor;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class RemoveSponsorCommandFaker
    {
        public static RemoveSponsorCommand GetCommandValid()
        {
            return new Faker<RemoveSponsorCommand>("pt_BR")
                .CustomInstantiator(f => new RemoveSponsorCommand(
                    f.Random.Number(1, 100)
                    )).Generate();
        }
    }
}