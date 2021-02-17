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
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, IResult>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var model = new EventModel(request.Id, request.Titulo, request.Descricao, request.DataInicio, request.DataFim, request.CategoriaId);

            try
            {
                _eventRepository.Update(model);

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