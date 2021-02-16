using Poc.Domain.Entities.Base;

namespace Poc.Domain.Entities
{
    public class CategoryModel : EntityBase
    {
        public CategoryModel(string descricao)
        {
            Descricao = descricao;
        }

        //ctor protected para EF
        protected CategoryModel() {}

        public string Descricao { get; private set; }
    }
}