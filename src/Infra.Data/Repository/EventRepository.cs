using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(DevEventsDbContext devEventsDbContext, IDapperBase dapperBase, ILogModel logModel) : base(devEventsDbContext, dapperBase, logModel)
        {
        }

        public async Task<IEnumerable<EventModel>> GetAllAsync()
        {
            try
            {
                var events = await _context.Events.AsNoTracking().ToListAsync();

                return events;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public async Task<EventModel> GetByIdAsync(int eventId)
        {
            try
            {
                var user = await _context.Events
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .SingleOrDefaultAsync(x => x.Id == eventId);

                return user;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Add(EventModel eventModel)
        {
            try
            {
                _context.Events.Add(eventModel);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Update(EventModel eventModel)
        {
            try
            {
                _context.Events.Update(eventModel);

                _context.Entry(eventModel).Property(e => e.DataCadastro).IsModified = false;
                _context.Entry(eventModel).Property(e => e.Ativo).IsModified = false;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Register(SubscriptionModel eventUserModel)
        {
            try
            {
                 _context.Subscription.AddAsync(eventUserModel);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Cancel(EventModel eventModel)
        {
            try
            {
                eventModel.DisableStatus(); 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public EventModel EventExists(int eventId)
        {
            try
            {
                var eventExists =  _context.Events.SingleOrDefault(x => x.Id == eventId);

                return eventExists; 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Remove(int eventId)
        {
            try
            {
                 _context.Events.RemoveRange(_context.Events.Where(x => x.Id.Equals(eventId))); 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void RemoveEventUser(int eventId)
        {
            try
            {
                _context.Subscription.RemoveRange(_context.Subscription.Where(x => x.EventoId.Equals(eventId)));
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool EventIdExists(int eventId)
        {
            try
            {
                var eventExists = _context.Events.Where(x => x.Id == eventId).Count();

                return eventExists > 0; 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool StatusIsFalse(int id)
        {
            try
            {         
                var status = _context.Events.Where(x => x.Id == id && x.Ativo == false).Count();

                return status <= 0;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool UserIdExists(int userId)
        {
            try
            {
                var userExists = _context.Users.Where(x => x.Id == userId).Count();

                return userExists > 0;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool HasEventToUser(int eventId, int userId)
        {
            try
            {
                //var count = (from e in _context.UsersEvents.AsNoTracking()
                //             where e.EventoId.Equals(eventId) &&
                //             e.UsuarioId.Equals(userId)
                //             select e)?.Count();

                var count = _context.Subscription.Where(x => x.EventoId == eventId && x.UsuarioId == userId).Count(); 

                return count > 0; //Se for maior que zero retorna True
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool TitleExists(string titulo)
        {
            try
            {
                var eventTitle = _context.Events.Where(x => x.Titulo == titulo).Count();

                return eventTitle <= 0; 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }
    }
}