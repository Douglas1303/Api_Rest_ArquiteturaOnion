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

        void AddAsync(EventModel eventModel);

        void UpdateAsync(EventModel eventModel);

        void RegisterAsync(EventUserModel eventUserModel);

        void CancelAsync(EventModel eventModel);
        EventModel EventExists(int eventId);

        void RemoveAsync(int eventId);
    }
}
