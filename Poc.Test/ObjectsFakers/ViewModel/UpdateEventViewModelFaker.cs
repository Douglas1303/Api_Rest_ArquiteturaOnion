using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class UpdateEventViewModelFaker 
    {
        public static UpdateEventViewModel GetViewModelValid()
        {
            return new Faker<UpdateEventViewModel>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Number(1, 999999))
            .RuleFor(x => x.Titulo, f => f.Lorem.Sentence(2))
            .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.DataInicio, f => f.Date.Recent().ToString())
            .RuleFor(x => x.DataFim, f => f.Date.Future().ToString())
            .RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true)
            .RuleFor(x => x.CategoriaId, f => f.Random.Number(1, 10))
            .Generate(); 
        }
    }
}