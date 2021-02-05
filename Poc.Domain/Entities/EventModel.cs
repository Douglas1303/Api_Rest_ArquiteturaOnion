using Poc.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Poc.Domain.Entities
{
    public class EventModel : EntityBase
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; set; }
        public CategoryModel Categoria { get; set; }
        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public IEnumerable<SubscriptionModel> Inscricoes { get; set; }

        public EventModel()
        {
            Ativo = true;           
        }
    }
}