using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;
using System;

namespace Poc.Domain.Commands.Events
{
    public class DisableEventCommand : Command
    {
        public DisableEventCommand(int eventoId)
        {
            EventoId = eventoId;
        }

        public int EventoId { get; private set; }
    }
}