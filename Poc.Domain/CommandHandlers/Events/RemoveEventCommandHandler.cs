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
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<RemoveEventCommandHandlerRsc> Localizer;

        private const string RemoveEventError = "RemoveEventError"; 

        public RemoveEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IStringLocalizer<RemoveEventCommandHandlerRsc> localizer)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer;
        }

        public async Task<IResult> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _eventRepository.Remove(request.EventoId);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(RemoveEventError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}