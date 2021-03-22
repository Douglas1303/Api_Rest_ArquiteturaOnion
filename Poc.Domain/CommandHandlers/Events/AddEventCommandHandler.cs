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
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddEventCommandHandlerRsc> Localizer;

        private const string AddEventError = "AddEventError"; 

        public AddEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, IStringLocalizer<AddEventCommandHandlerRsc> localizer)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var model = new EventModel(request.Titulo, request.Descricao, request.DataInicio, request.DataFim, request.CategoriaId);

            try
            {
                _eventRepository.Add(model);

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(AddEventError));

                return cmdResult; 
            }

            return CommandResult.Empty();
        }
    }
}