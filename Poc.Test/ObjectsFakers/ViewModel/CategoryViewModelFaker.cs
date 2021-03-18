using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class CategoryViewModelFaker : Faker<CategoryViewModel>
    {
        public CategoryViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            RuleFor(x => x.Id, f => id);
            RuleFor(x => x.Descricao, f => f.Lorem.Sentence(20));
            RuleFor(x => x.DataCadastro, f => f.Date.Recent()); 
        }
    }
}