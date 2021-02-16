using Poc.Domain.Entities.Base;

namespace Poc.Domain.Entities
{
    public class SubscriptionModel : EntityBase
    {
        public SubscriptionModel(int usuarioId, int eventoId)
        {
            UsuarioId = usuarioId;
            EventoId = eventoId;
        }

        //ctor protected para EF
        protected SubscriptionModel() { }

        public int UsuarioId { get; private set; }
        public UserModel Usuario { get; private set; }
        public int EventoId { get; private set; }
        public EventModel Evento { get; private set; }
    }
}