using Bogus;
using Poc.Application.ViewModel.Identity;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class LoginIdentityViewModelFaker : Faker<LoginIdentityViewModel>
    {
        public LoginIdentityViewModelFaker()
        {
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.Password, f => f.Internet.Password());
        }
    }
}