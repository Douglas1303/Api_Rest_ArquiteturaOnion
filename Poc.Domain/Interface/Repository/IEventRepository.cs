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

        void Add(EventModel eventModel);

        void Update(EventModel eventModel);

        void Register(SubscriptionModel eventUserModel);

        void Cancel(EventModel eventModel);
        EventModel EventExists(int eventId);

        void Remove(int eventId);
        void RemoveEventUser(int eventId);

        bool EventIdExists(int eventId);

        bool TituloExists(string titulo);

        bool UserIdExists(int userId);

        bool HasEventToUser(int eventId, int userId); 
    }
}
