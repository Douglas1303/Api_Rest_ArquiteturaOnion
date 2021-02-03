using Poc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetCategories();
    }
}