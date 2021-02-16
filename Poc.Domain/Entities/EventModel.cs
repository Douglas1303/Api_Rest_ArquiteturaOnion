using Poc.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Poc.Domain.Entities
{
    public class EventModel : EntityBase
    {
        public EventModel(string titulo, string descricao, DateTime dataInicio, DateTime dataFim, int? categoriaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Ativo = true;
            CategoriaId = categoriaId;
        }

        public EventModel(int id, string titulo, string descricao, DateTime dataInicio, DateTime dataFim, int? categoriaId)
        {
            Id = id; 
            Titulo = titulo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            CategoriaId = categoriaId;
        }

        //ctor protected para EF
        protected EventModel() {}

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; private set; }
        public CategoryModel Categoria { get; private set; }
        public IEnumerable<UserModel> Usuarios { get; private set; }
        public IEnumerable<SubscriptionModel> Inscricoes { get; private set; }
    }
}