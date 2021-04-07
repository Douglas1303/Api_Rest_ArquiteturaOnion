using Bogus;
using Poc.Application.ViewModel;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class CategoryViewModelFaker 
    {
        public static List<CategoryViewModel> GetViewModelValid()
        {
            return new Faker<CategoryViewModel>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Number(1, 9999999))
            .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(20))
            .RuleFor(x => x.DataCadastro, f => f.Date.Recent())
            .Generate(6); 
        }
    }
}