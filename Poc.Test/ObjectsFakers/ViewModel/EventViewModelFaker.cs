using Bogus;
using Poc.Application.ViewModel;

namespace Poc.Test.ObjectsFakers.ViewModel
{
    public class EventViewModelFaker : Faker<EventViewModel>
    {
        public EventViewModelFaker()
        {
            var id = new Faker().Random.Number(1, 999999);
            var categoriaId = new Faker().Random.Number(1, 10);

            RuleFor(x => x.Id, f => id);
            RuleFor(x => x.DataCadastro, f => f.Date.Recent());
            RuleFor(x => x.Titulo, f => f.Lorem.Sentence(2));
            RuleFor(x => x.Descricao, f => f.Lorem.Sentence(5));
            RuleFor(x => x.DataInicio, f => f.Date.Recent()); 
            RuleFor(x => x.DataFim, f => f.Date.Future()); 
            RuleFor(x => x.Ativo, f => f.IndexFaker == 0 ? false : true);
            RuleFor(x => x.CategoriaId, f => categoriaId); 
        }
    }
}