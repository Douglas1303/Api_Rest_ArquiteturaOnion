using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Domain.Dtos;
using Poc.Domain.Enum;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.Dtos
{
    public static class SponsorDtoFaker
    {
        public static List<SponsorDto> GetListDtoValid()
        {
            return new Faker<SponsorDto>("pt_BR")
                .CustomInstantiator(f => new SponsorDto(
                    f.Person.FullName,
                    f.Person.Cpf().Replace("-", "").Replace(".", ""),
                    f.Person.Phone,
                    f.Address.ZipCode(),
                    f.Address.StreetName(),
                    f.Address.StreetSuffix(),
                    f.Lorem.Sentence(2),
                    f.Address.City(),
                    f.PickRandom<EStates>().ToString(),
                    f.Random.Number(10, 60)
                    )).Generate(5);
        }
    }
}