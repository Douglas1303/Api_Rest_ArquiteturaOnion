using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface ICategoryApplication
    {
        Task<IResult> GetAllAsync();
        Task<IResult> AddCategory(AddCategoryViewModel addCategoryViewModel); 
    }
}