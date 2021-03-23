using Bogus;
using Poc.Domain.Commands.File;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class AddFileCommandFaker
    {
        public static AddFileCommand GetCommandValid()
        {
            return new Faker<AddFileCommand>("pt_BR")
                .CustomInstantiator(f => new AddFileCommand(
                    f.Random.Number(1, 100),
                    f.Lorem.Sentence(2),
                    f.Lorem.Sentence(3)
                    )).Generate(); 
        }
    }
}