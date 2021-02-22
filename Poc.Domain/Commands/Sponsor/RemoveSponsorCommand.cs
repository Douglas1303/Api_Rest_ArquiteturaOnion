using Infra.CrossCutting.Core.CQRS.Command;

namespace Poc.Domain.Commands.Sponsor
{
    public class RemoveSponsorCommand : Command
    {
        public RemoveSponsorCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}