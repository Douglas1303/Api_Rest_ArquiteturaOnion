using Bogus;
using Poc.Application.ViewModel.Identity;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class UserIdentityViewModelFaker : Faker<UserIdentityViewModel>
    {
        public UserIdentityViewModelFaker()
        {
            var password = new Faker().Internet.Password();

            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.Password, f => password);
            RuleFor(x => x.ConfirmPassword, f => password);
        }
    }
}