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
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var model = new EventModel(request.Titulo, request.Descricao, request.DataInicio, request.DataFim, request.CategoriaId);

            try
            {
                _eventRepository.Add(model);

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