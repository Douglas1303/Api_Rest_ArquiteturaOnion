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
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<UpdateEventCommandHandlerRsc> Localizer;

        private const string UpdateEventError = "UpdateEventError"; 

        public UpdateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IStringLocalizer<UpdateEventCommandHandlerRsc> localizer)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer;
        }

        public async Task<IResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var model = new EventModel(request.Id, request.Titulo, request.Descricao, request.DataInicio, request.DataFim, request.CategoriaId);

            try
            {
                _eventRepository.Update(model);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(UpdateEventError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}