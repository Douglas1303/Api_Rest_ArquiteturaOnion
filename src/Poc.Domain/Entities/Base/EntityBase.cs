using System;

namespace Poc.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }

        public EntityBase()
        {
            DataCadastro = DateTime.Now; 
        }
    }
}