using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Events;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Events
{
    public class RegisterEventUserCommandHandler : IRequestHandler<RegisterEventUserCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<RegisterEventUserCommandHandlerRsc> Localizer;

        private const string RegisterEventError = "RegisterEventError"; 
        private const string RegisteredUserInfo  = "RegisteredUserInfo"; 

        public RegisterEventUserCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IStringLocalizer<RegisterEventUserCommandHandlerRsc> localizer)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(RegisterEventUserCommand request, CancellationToken cancellationToken)
        {
            var model = new SubscriptionModel(request.UsuarioId, request.EventoId);

            try
            {
                if (_eventRepository.HasEventToUser(request.EventoId, request.UsuarioId))
                {
                    return new CommandResult(Localizer.GetMsg(RegisteredUserInfo));
                }

                _eventRepository.Register(model);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(RegisterEventError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}