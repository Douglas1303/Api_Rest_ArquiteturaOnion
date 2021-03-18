using Bogus;
using Poc.Domain.Entities;

namespace Poc.Test.ObjectsFakers.Entities
{
    public class CategoryModelFaker : Faker<CategoryModel>
    {
        public CategoryModelFaker()
        {
            RuleFor(x => x.Id, f => f.Random.Number(1, 99999));
            RuleFor(x => x.DataCadastro, f => f.Date.Past());
            RuleFor(x => x.Descricao, f => f.Lorem.Sentence(3));
        }
    }
}