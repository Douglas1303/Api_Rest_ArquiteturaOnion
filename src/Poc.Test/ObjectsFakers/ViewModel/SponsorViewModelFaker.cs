using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Application.ViewModel;
using Poc.Domain.Enum;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class SponsorViewModelFaker 
    {
        public static List<SponsorViewModel> GetViewModelValid()
        {
            return new Faker<SponsorViewModel>("pt_BR")
            .RuleFor(x => x.NomePatrocinador, f => f.Person.FirstName)
            .RuleFor(x => x.Documento, f => f.Person.Cpf())
            .RuleFor(x => x.Telefone, f => f.Person.Phone)
            .RuleFor(x => x.Cep, f => f.Address.ZipCode().Replace("-", ""))
            .RuleFor(x => x.Logradouro, f => f.Address.StreetName())
            .RuleFor(x => x.Complemento, f => f.Lorem.Sentence(2))
            .RuleFor(x => x.Localidade, f => f.Address.City())
            .RuleFor(x => x.Bairro, f => f.Lorem.Sentence(2))
            .RuleFor(x => x.UF, f => f.PickRandom<EStates>().ToString())
            .RuleFor(x => x.DDD, f => f.Random.Number(1, 60))
            .Generate(10);          
        }
    }
}