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
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        public void AddAsync(EventModel eventModel)
        {
            try
            {
                _context.Events.AddAsync(eventModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAsync(EventModel eventModel)
        {
            try
            {
                _context.Events.Update(eventModel);

                _context.Entry(eventModel).Property(e => e.DataCadastro).IsModified = false;
                _context.Entry(eventModel).Property(e => e.Ativo).IsModified = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RegisterAsync(EventUserModel eventUserModel)
        {
            try
            {
                 _context.UsersEvents.AddAsync(eventUserModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelAsync(EventModel eventModel)
        {
            try
            {
                eventModel.Ativo = false;
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
        }

        public void RemoveAsync(int eventId)
        {
            try
            {
                 _context.Events.RemoveRange(_context.Events.Where(x => x.Id.Equals(eventId))); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}