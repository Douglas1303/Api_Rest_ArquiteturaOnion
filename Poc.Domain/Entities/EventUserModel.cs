namespace Poc.Domain.Entities
{
    public class EventUserModel
    {
        public int EventoId { get; set; }
        public virtual EventModel Evento { get; set; }
        public int UsuarioId { get; set; }
        public virtual UserModel Usuario { get; set; }
    }
}