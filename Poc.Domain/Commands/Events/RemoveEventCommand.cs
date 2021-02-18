using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Events
{
    public class RemoveEventCommand : Command
    {
        public RemoveEventCommand(int eventoId)
        {
            EventoId = eventoId;
        }

        public int EventoId { get; set; }
    }
}