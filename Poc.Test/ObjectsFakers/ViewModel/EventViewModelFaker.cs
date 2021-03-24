using Bogus;
using Poc.Application.ViewModel;
using System.Collections.Generic;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public static class EventViewModelFaker 
    {
        public static EventViewModel GetViewModelValid()
        {
            return new Faker<EventViewModel>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Number(1, 999999))
            .RuleFor(x => x.DataCadastro, f => f.Date.Recent())
            .RuleFor(x => x.Titulo, f => f.Lorem.Sentence(2))
            .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.DataInicio, f => f.Date.Recent())
            .RuleFor(x => x.DataFim, f => f.Date.Future())
            .RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true)
            .RuleFor(x => x.CategoriaId, f => f.Random.Number(1, 10))
            .Generate(); 
        }

        public static List<EventViewModel> GetListViewModelValid()
        {
            return new Faker<EventViewModel>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Number(1, 999999))
            .RuleFor(x => x.DataCadastro, f => f.Date.Recent())
            .RuleFor(x => x.Titulo, f => f.Lorem.Sentence(2))
            .RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.DataInicio, f => f.Date.Recent())
            .RuleFor(x => x.DataFim, f => f.Date.Future())
            .RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true)
            .RuleFor(x => x.CategoriaId, f => f.Random.Number(1, 10))
            .Generate(5);
        }
    }
}