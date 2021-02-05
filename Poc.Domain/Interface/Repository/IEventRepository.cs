using Poc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventModel>> GetAllAsync();

        Task<EventModel> GetByIdAsync(int eventId);

        Task<EventModel> AddAsync(EventModel eventModel);

        Task<EventModel> UpdateAsync(EventModel eventModel);

        Task<EventModel> RegisterAsync(int eventId, int userId, EventModel eventModel);

        Task<string> CancelAsync(EventModel eventModel);
    }
}
