using Bogus;
using Poc.Domain.Commands.Categories;

namespace Poc.Test.ObjectsFakers.Command
{
    public static class AddCategoryCommandFaker
    {
        public static AddCategoryCommand GetCommandValid()
        {
            return new Faker<AddCategoryCommand>("pt_BR")
                .CustomInstantiator(f => new AddCategoryCommand(
                    f.Lorem.Sentence(3)
                    )).Generate(); 
        }
    }
}