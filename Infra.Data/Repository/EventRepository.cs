using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
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
                    .Include(x => x.Usuario)
                    .SingleOrDefaultAsync(x => x.Id == eventId);

                return user; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EventModel> AddAsync(EventModel eventModel)
        {
            try
            {
                var addEvent = await _context.Events.AddAsync(eventModel);
               var save = await _context.SaveChangesAsync();

                return eventModel; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EventModel> UpdateAsync(EventModel eventModel)
        {
            try
            {
                  _context.Events.Update(eventModel);

                _context.Entry(eventModel).Property(e => e.DataCadastro).IsModified = false;
                _context.Entry(eventModel).Property(e => e.Ativo).IsModified = false;
                _context.Entry(eventModel).Property(e => e.UsuarioId).IsModified = false;

                await _context.SaveChangesAsync(); 

                return eventModel; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<EventModel> RegisterAsync(int eventId, int userId, EventModel eventModel)
        {
            try
            {
                return null; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> CancelAsync(EventModel eventModel)
        {
            try
            {
                eventModel.Ativo = false;

               await _context.SaveChangesAsync();

                return "Evento desativado."; 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}