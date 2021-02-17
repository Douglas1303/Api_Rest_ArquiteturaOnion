using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Events;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Events
{
    public class RegisterEventUserCommandHandler : IRequestHandler<RegisterEventUserCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterEventUserCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(RegisterEventUserCommand request, CancellationToken cancellationToken)
        {
            var model = new SubscriptionModel(request.UsuarioId, request.EventoId);

            try
            {
                if (_eventRepository.HasEventToUser(request.EventoId, request.UsuarioId))
                {
                    return new CommandResult("Usuario já cadastrado para o evento.");
                }

                _eventRepository.Register(model);

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