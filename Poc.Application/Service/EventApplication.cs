using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Events;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class EventApplication : BaseApplicationService, IEventApplication
    {
        private readonly IEventRepository _eventRepository;
        private readonly IStringLocalizer<EventAppRsc> Localizer;

        #region Constantes
        private const string GetAllEventError = "GetAllEventError";
        private const string GetByIdEventError = "GetByIdEventError";
        private const string AddEventError = "AddEventError"; 
        private const string UpdateEventError = "UpdateEventError"; 
        private const string RegisterUserEventError = "RegisterUserEventError"; 
        private const string CancelEventError = "CancelEventError"; 
        private const string RemoveEventError = "RemoveEventError";
        #endregion

        public EventApplication(IEventRepository eventRepository, IMediatorHandler mediatorHandler, IMapper mapper, 
                                ILogModel logModel, IStringLocalizer<EventAppRsc> localizer) : base(mediatorHandler, mapper, logModel)
        {
            _eventRepository = eventRepository;
            Localizer = localizer; 
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<EventViewModel>>(await _eventRepository.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(GetAllEventError)); 
                throw ex;
            }
        }

        public async Task<IResult> GetByIdAsync(int eventId)
        {
            try
            {
                var result = _mapper.Map<EventViewModel>(await _eventRepository.GetByIdAsync(eventId)); 

                if(result == null) return new QueryResult("Evento não existe."); 

                return new QueryResult(result);
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(GetByIdEventError)); 
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
                return new QueryResult(Localizer.GetMsg(AddEventError)); 
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
                return new QueryResult(Localizer.GetMsg(UpdateEventError)); 
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
                return new QueryResult(Localizer.GetMsg(RegisterUserEventError)); 
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
                return new QueryResult(Localizer.GetMsg(CancelEventError));
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
                return new QueryResult(Localizer.GetMsg(RemoveEventError)); 
                throw ex;
            }
        }
    }
}