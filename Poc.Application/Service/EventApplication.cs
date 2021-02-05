using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Poc.Application.Interface;
using Poc.Application.Service.Base;
using Poc.Application.ViewModel;
using Poc.Domain.Entities;
using Poc.Domain.Helper.Interface;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class EventApplication : BaseApplicationService, IEventApplication
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserInfo _userInfo; 

        public EventApplication(IEventRepository eventRepository, IUserInfo userInfo, IMediatorHandler mediatorHandler, IMapper mapper, ILogModel logModel) : base(mediatorHandler, mapper, logModel)
        {
            _eventRepository = eventRepository;
            _userInfo = userInfo; 
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(await _eventRepository.GetAllAsync()); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResult> GetByIdAsync(int eventId)
        {
            try
            {
                return new QueryResult(await _eventRepository.GetByIdAsync(eventId)); 
            }
            catch (Exception)
            {

                throw;
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
                    UsuarioId = _userInfo.UserId
                }; 

                return new QueryResult(await _eventRepository.AddAsync(model)); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IResult> UpdateAsync(UpdateEventViewModel eventViewModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult> RegisterAsync(int eventId, int userId, AddUserEventViewModel eventViewModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IResult> CancelAsync(int eventId)
        {
            try
            {
                var events = await _eventRepository.GetByIdAsync(eventId);

                if (events == null) return new QueryResult("Evento não existe.");

                return new QueryResult(_eventRepository.CancelAsync(events)); 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}