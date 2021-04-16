using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Events;
using Poc.Domain.Helper.Interface;
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
        private readonly IAuthenticatedUser _user;
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

        public EventApplication(IEventRepository eventRepository, IAuthenticatedUser user, IMediatorHandler mediatorHandler, IMapper mapper, 
                                ILogModel logModel, IStringLocalizer<EventAppRsc> localizer) : base(mediatorHandler, mapper, logModel)
        {
            _eventRepository = eventRepository;
            _user = user; 
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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(GetAllEventError)); 
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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(GetByIdEventError)); 
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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(AddEventError)); 

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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(UpdateEventError)); 
            }
        }

        public async Task<IResult> RegisterAsync(int eventId)
        {
            try
            {
                var command = new RegisterEventUserCommand(
                    _user.UserId,
                    eventId);

                return await _mediatorHandler.SendCommand(command);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(RegisterUserEventError)); 
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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(CancelEventError));
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
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(RemoveEventError)); 
            }
        }
    }
}