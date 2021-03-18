using Bogus;
using ExternalServices.Cep;
using Poc.Domain.Enum;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class CepViewModelFaker : Faker<CepViewModel>
    {
        public CepViewModelFaker()
        {
            var ddd = new Faker().Random.Number(1, 60);

            RuleFor(x => x.Cep, f => f.Address.ZipCode().Replace("-", ""));
            RuleFor(x => x.Logradouro, f => f.Address.StreetName());
            RuleFor(x => x.Complemento, f => f.Lorem.Sentence(2));
            RuleFor(x => x.Localidade, f => f.Address.City());
            RuleFor(x => x.Bairro, f => f.Lorem.Sentence(2));
            RuleFor(x => x.UF, f => f.PickRandom<EStates>().ToString());
            RuleFor(x => x.DDD, f => ddd.ToString());
        }
    }
}