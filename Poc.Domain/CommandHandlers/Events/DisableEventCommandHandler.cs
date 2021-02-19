using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Events;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Events
{
    public class DisableEventCommandHandler : IRequestHandler<DisableEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(DisableEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var events = _eventRepository.EventExists(request.EventoId);

                _eventRepository.Cancel(events);

                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CommandResult.Empty();
        }
    }
}