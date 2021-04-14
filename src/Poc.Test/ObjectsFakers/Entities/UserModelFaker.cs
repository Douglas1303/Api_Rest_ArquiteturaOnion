using Bogus;
using Bogus.Extensions.Brazil;
using Poc.Domain.Entities;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.Entities
{
    public static class UserModelFaker
    {
        public static List<UserModel> GetModelValid()
        {
            return new Faker<UserModel>("pt_BR")
                .CustomInstantiator(f => new UserModel(
                    f.Random.Guid(),
                    f.Person.FullName, 
                    f.Person.Cpf(),
                    f.Date.Past().AddYears(-18),
                    f.Person.Email
                    )).Generate(4);
        }
    }
}