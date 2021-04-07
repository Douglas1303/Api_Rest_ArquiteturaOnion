using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System.Linq;
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

        public void Rollback()
        {
            var changedEntries = _devEventsDbContext.ChangeTracker.Entries()
               .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _devEventsDbContext.Dispose();
        }
    }
}