using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Events
{
    public class RegisterEventUserCommand : Command
    {
        public RegisterEventUserCommand(int usuarioId, int eventoId)
        {
            UsuarioId = usuarioId;
            EventoId = eventoId;
        }

        public int UsuarioId { get; set; }
        public int EventoId { get; set; }
    }
}