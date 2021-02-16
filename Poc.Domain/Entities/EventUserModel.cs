namespace Poc.Domain.Entities
{
    public class EventUserModel
    {
        public EventUserModel(int eventoId, int usuarioId)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
        }

        //ctor protected para EF
        protected EventUserModel() {}

        public int EventoId { get; private set; }
        public virtual EventModel Evento { get; private set; }
        public int UsuarioId { get; private set; }
        public virtual UserModel Usuario { get; private set; }
    }
}