using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Enum;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class AddSponsorCommandFaker
    {
        public static AddSponsorCommand GetCommandValid()
        {
            return new Faker<AddSponsorCommand>("pt_BR")
                .CustomInstantiator(f => new AddSponsorCommand(
                    f.PickRandom<ETipoPatrocinador>(),
                    f.Person.FullName,
                    f.Person.Cpf().Replace("-", "").Replace(".", ""),
                    f.Person.Phone,
                    f.Address.ZipCode().Replace("-", ""),
                    f.Address.StreetName(),
                    f.Lorem.Sentence(2),
                    f.Lorem.Sentence(2),
                    f.Address.City(),
                    f.PickRandom<EStates>().ToString(),
                    f.Random.Number(10, 60)
                    )).Generate();
        }
    }
}