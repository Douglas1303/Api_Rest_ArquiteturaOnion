using Bogus;
using Poc.Domain.Entities;

namespace Poc.Test.ObjectsFakers.Entities
{
    public static class EventModelFaker
    {
        public static EventModel GetModelValid()
        {
            return new Faker<EventModel>("pt_BR")
                .CustomInstantiator(f => new EventModel(
                    f.Random.Number(1, 100),
                    f.Lorem.Sentence(2),
                    f.Lorem.Sentence(4),
                    f.Date.Recent().AddDays(1),
                    f.Date.Recent().AddDays(4),
                    f.Random.Number(1, 10)
                    )).Generate();
        }
    }
}