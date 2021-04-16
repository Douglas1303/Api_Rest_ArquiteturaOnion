using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using MediatR;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}