﻿using Poc.Domain.Entities;
using System;
using System.Collections.Generic;
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

        bool TitleExists(string titulo);

        bool UserIdExists(Guid userId);

        bool HasEventToUser(int eventId, Guid userId);

        bool StatusIsFalse(int id);

        bool CategoryExists(int categoryId);
    }
}