using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class AddEventViewModelFaker
    {
        public static AddEventViewModel GetViewModelValid()
        {
            return new Faker<AddEventViewModel>("pt_BR")
                .RuleFor(x => x.Titulo, f => f.Lorem.Sentence(2))
                .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5))
                .RuleFor(x => x.DataInicio, f => f.Date.Recent().ToString())
                .RuleFor(x => x.DataFim, f => f.Date.Future().ToString())
                .RuleFor(x => x.CategoriaId, f => f.Random.Number(1, 10)).Generate();
        }
    }
}