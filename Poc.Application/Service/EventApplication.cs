using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Infra.CrossCutting.Extensions; 
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using Poc.Domain.Helper.Interface;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class EventApplication : BaseApplicationService, IEventApplication
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserInfo _userInfo;
        private readonly IUnitOfWork _unitOfWork; 

        public EventApplication(IEventRepository eventRepository, IUserInfo userInfo, IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler, IMapper mapper, ILogModel logModel) : base(mediatorHandler, mapper, logModel)
        {
            _eventRepository = eventRepository;
            _userInfo = userInfo;
            _unitOfWork = unitOfWork; 
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
                var model = new EventModel 
                {
                    Titulo = eventViewModel.Titulo,
                    Descricao = eventViewModel.Descricao, 
                    DataInicio = DateTime.Parse(eventViewModel.DataInicio),
                    DataFim = DateTime.Parse(eventViewModel.DataFim),
                    CategoriaId = eventViewModel.CategoriaId
                 };

                 _eventRepository.Add(model);

                await _unitOfWork.Commit(); 

                return new CommandResult(); 
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
                var model = new EventModel
                {
                    Id = eventViewModel.Id, 
                    Titulo = eventViewModel.Titulo, 
                    Descricao = eventViewModel.Descricao, 
                    DataInicio = DateTime.Parse(eventViewModel.DataInicio), 
                    DataFim = DateTime.Parse(eventViewModel.DataFim), 
                    CategoriaId = eventViewModel.CategoriaId
                };

                _eventRepository.Update(model);

                await _unitOfWork.Commit(); 

                return new CommandResult(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IResult> RegisterAsync(AddUserEventViewModel addUserEventViewModel)
        {

            //CheckEventIdExists(addUserEventViewModel); 
            //CheckUserIdExists(addUserEventViewModel); 

            try
            {

                if (_eventRepository.HasEventToUser(addUserEventViewModel.EventoId, addUserEventViewModel.UsuarioId))
                {
                    return new CommandResult("Usuario já cadastrado para o evento.");
                }


                _eventRepository.RemoveEventUser(addUserEventViewModel.EventoId); 

                var model = new EventUserModel
                {
                    EventoId = addUserEventViewModel.EventoId, 
                    UsuarioId = addUserEventViewModel.UsuarioId
                }; 

                //TODO: Validar se Usuario Id e EventoId Existe na base de dados. 

                 _eventRepository.Register(model);

                await _unitOfWork.Commit(); 

                return new CommandResult(); 
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
                var events = _eventRepository.EventExists(eventId);

                if (events == null) return new QueryResult("Evento não existe.");

                 _eventRepository.Cancel(events);

                await _unitOfWork.Commit(); 

                return new CommandResult(); 
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
                _eventRepository.Remove(eventId);

                await _unitOfWork.Commit(); 

                return new CommandResult(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //refatorar para command
        private void CheckEventIdExists(AddUserEventViewModel addUserEventViewModel)
        {
            try
            {
                if (!_eventRepository.EventIdExists(addUserEventViewModel.EventoId))
                {
                     new QueryResult("EventoId não existe.");
                }
            }
            catch (Exception ex)
            {

                throw  ex;
            }
        }

        private void CheckUserIdExists(AddUserEventViewModel addUserEventViewModel)
        {
            try
            {
                if (!_eventRepository.UserIdExists(addUserEventViewModel.UsuarioId))
                {
                    new QueryResult("UsuarioId não existe.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}