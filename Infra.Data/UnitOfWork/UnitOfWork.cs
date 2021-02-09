using Infra.Data.Context;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System.Threading.Tasks;

namespace Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevEventsDbContext _devEventsDbContext;

        public UnitOfWork(DevEventsDbContext devEventsDbContext)
        {
            _devEventsDbContext = devEventsDbContext;
        }

        public async Task Commit()
        {
            await _devEventsDbContext.SaveChangesAsync();
        }

        public Task Rollback()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _devEventsDbContext.Dispose();
        }
    }
}