using Bogus;
using Poc.Application.ViewModel.Identity;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class LoginIdentityViewModelFaker 
    {
        public static LoginIdentityViewModel GetViewModelValid()
        {
            return new Faker<LoginIdentityViewModel>("pt_BR")
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => f.Internet.Password())
            .Generate(); 
        }
    }
}