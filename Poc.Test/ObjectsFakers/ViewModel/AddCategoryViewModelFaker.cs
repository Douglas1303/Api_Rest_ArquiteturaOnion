using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class AddCategoryViewModelFaker : Faker<AddCategoryViewModel>
    {
        public AddCategoryViewModelFaker()
        {
            RuleFor(x => x.Descricao, f => f.Lorem.Sentence(3)); 
        }
    }
}