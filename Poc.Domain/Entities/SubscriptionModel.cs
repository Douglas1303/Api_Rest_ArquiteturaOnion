using Poc.Domain.Entities.Base;

namespace Poc.Domain.Entities
{
    public class SubscriptionModel : EntityBase
    {
        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public int EventoId { get; set; }
        public EventModel Evento { get; set; }
    }
}