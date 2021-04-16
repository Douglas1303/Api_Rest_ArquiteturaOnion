using Bogus;
using Poc.Application.ViewModel.Identity;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class UserIdentityViewModelFaker 
    {
        public static UserIdentityViewModel GetViewModelValid()
        {
            var password = new Faker().Internet.Password();

            return new Faker<UserIdentityViewModel>("pt_BR")
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => password)
            .RuleFor(x => x.ConfirmPassword, f => password)
            .Generate(); 
        }
    }
}