using Poc.Domain.Entities.Base;
using System;

namespace Poc.Domain.Entities
{
    public class SubscriptionModel 
    {
        public SubscriptionModel(int usuarioId, int eventoId)
        {
            UsuarioId = usuarioId;
            EventoId = eventoId;
            DataCadastro = DateTime.Now; 
        }

        //ctor protected para EF
        protected SubscriptionModel() { }

        public int UsuarioId { get; private set; }
        public UserModel Usuario { get; private set; }
        public int EventoId { get; private set; }
        public EventModel Evento { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}