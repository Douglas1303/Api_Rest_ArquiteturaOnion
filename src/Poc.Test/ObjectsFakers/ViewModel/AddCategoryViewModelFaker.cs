using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class AddCategoryViewModelFaker 
    {
        public static AddCategoryViewModel GetViewModelValid()
        {
            return new Faker<AddCategoryViewModel>("pt_BR")
                .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(3))
                .Generate(); 
        }
    }
}