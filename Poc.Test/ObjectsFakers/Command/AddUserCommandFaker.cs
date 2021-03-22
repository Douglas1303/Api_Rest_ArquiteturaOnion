using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Domain.Commands.Users;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class AddUserCommandFaker
    {
        public static AddUserCommand GetCommandValid()
        {
            return new Faker<AddUserCommand>("pt_BR")
                .CustomInstantiator(f => new AddUserCommand(
                    f.Person.FullName,
                    f.Person.Cpf().Replace("-", "").Replace(".", ""),
                    f.Date.Past(20).AddYears(-18),
                    f.Person.Email
                    )).Generate();
        }

        public static AddUserCommand GetCommandCpfEmpty()
        {
            return new Faker<AddUserCommand>("pt_BR")
                .CustomInstantiator(f => new AddUserCommand(
                    f.Person.FullName,
                    string.Empty,
                    f.Date.Past(20).AddYears(-18),
                    f.Person.Email
                    )).Generate();
        }

        public static AddUserCommand GetCommandCpfFormatInvalid()
        {
            return new Faker<AddUserCommand>("pt_BR")
                .CustomInstantiator(f => new AddUserCommand(
                    f.Person.FullName,
                    "1111111111111",
                    f.Date.Past(20).AddYears(-18),
                    f.Person.Email
                    )).Generate();
        }
    }
}