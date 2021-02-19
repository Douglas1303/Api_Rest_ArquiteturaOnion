using Infra.CrossCutting.Core.CQRS.Events;
using MediatR;
using System;

namespace Infra.CrossCutting.Core.CQRS.Command
{
    public abstract class Command : Message, IRequest<IResult>
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}