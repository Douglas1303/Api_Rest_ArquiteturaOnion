using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class UserViewModelFaker : Faker<UserViewModel>
    {
        public UserViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);

            RuleFor(x => x.Id, f => id); 
            RuleFor(x => x.DataCadastro, f => f.Date.Recent()); 
            RuleFor(x => x.NomeCompleto, f => f.Person.FirstName); 
            RuleFor(x => x.Cpf, f => f.Person.Cpf()); 
            RuleFor(x => x.DataNascimento, f => f.Date.Past()); 
            RuleFor(x => x.Email, f => f.Person.Email); 
            RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true); 

        }
    }
}