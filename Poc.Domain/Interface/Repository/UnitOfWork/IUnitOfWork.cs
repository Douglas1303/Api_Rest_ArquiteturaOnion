using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Rollback(); 
    }
}