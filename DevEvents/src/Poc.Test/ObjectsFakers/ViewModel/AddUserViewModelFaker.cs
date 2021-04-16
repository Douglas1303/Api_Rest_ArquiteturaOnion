using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class AddUserViewModelFaker 
    {
        public static AddUserViewModel GetViewModelValid()
        {
            return new Faker<AddUserViewModel>("pt_BR")
            .RuleFor(x => x.NomeCompleto, f => f.Person.FullName)
            .RuleFor(x => x.Cpf, f => f.Person.Cpf())
            .RuleFor(x => x.DataNascimento, f => f.Date.Past().AddYears(-18).ToString())
            .RuleFor(x => x.Cep, f => f.Address.ZipCode().Replace("-", ""))
            .Generate(); 
        }
    }
}