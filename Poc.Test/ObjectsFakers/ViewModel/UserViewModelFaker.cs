using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Application.ViewModel;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class UserViewModelFaker 
    {
        public static List<UserViewModel> GetListViewModelValid()
        {
            var id = new Faker().Random.Number(1, 999999);

            return new Faker<UserViewModel>("pt_BR")
            .RuleFor(x => x.Id, f => id)
            .RuleFor(x => x.DataCadastro, f => f.Date.Recent())
            .RuleFor(x => x.NomeCompleto, f => f.Person.FirstName)
            .RuleFor(x => x.Cpf, f => f.Person.Cpf())
            .RuleFor(x => x.DataNascimento, f => f.Date.Past())
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true)
            .Generate(6); 

        }
    }
}