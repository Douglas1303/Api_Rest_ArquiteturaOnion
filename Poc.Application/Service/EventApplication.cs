using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Events;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class EventApplication : BaseApplicationService, IEventApplication
    {
        private readonly IEventRepository _eventRepository;

        public EventApplication(IEventRepository eventRepository, IMediatorHandler mediatorHandler, IMapper mapper, ILogModel logModel) : base(mediatorHandler, mapper, logModel)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(await _eventRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> GetByIdAsync(int eventId)
        {
            try
            {
                return new QueryResult(await _eventRepository.GetByIdAsync(eventId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> AddAsync(AddEventViewModel eventViewModel)
        {
            try
            {
                var command = new AddEventCommand(
                    eventViewModel.Titulo,
                    eventViewModel.Descricao,
                    DateTime.Parse(eventViewModel.DataInicio),
                    DateTime.Parse(eventViewModel.DataFim),
                    eventViewModel.CategoriaId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> UpdateAsync(UpdateEventViewModel eventViewModel)
        {
            try
            {
                var command = new UpdateEventCommand(
                    eventViewModel.Id,
                    eventViewModel.Titulo,
                    eventViewModel.Descricao,
                    DateTime.Parse(eventViewModel.DataInicio),
                    DateTime.Parse(eventViewModel.DataFim),
                    eventViewModel.Ativo,
                    eventViewModel.CategoriaId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> RegisterAsync(AddUserEventViewModel addUserEventViewModel)
        {
            try
            {
                var command = new RegisterEventUserCommand(
                    addUserEventViewModel.UsuarioId,
                    addUserEventViewModel.EventoId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> CancelAsync(int eventId)
        {
            try
            {
                var command = new DisableEventCommand(eventId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IResult> RemoveAsync(int eventId)
        {
            try
            {
                var command = new RemoveEventCommand(eventId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}