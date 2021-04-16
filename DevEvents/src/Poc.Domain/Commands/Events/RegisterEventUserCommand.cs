using FluentValidation.Results;
using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Events
{
    public class RegisterEventUserCommand : Command
    {
        public RegisterEventUserCommand(string userId, int eventId)
        {
            UserId = userId;
            EventId = eventId;
        }

        public string UserId { get; private set; }
        public int EventId { get; private set; }
    }
}