using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Events;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Events
{
    public class DisableEventCommandHandler : IRequestHandler<DisableEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DisableEventCommandHandlerRsc> Localizer;

        private const string DisableEventError = "DisableEventError"; 

        public DisableEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IStringLocalizer<DisableEventCommandHandlerRsc> localizer)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(DisableEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var events = _eventRepository.EventExists(request.EventoId);

                _eventRepository.Cancel(events);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(DisableEventError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}