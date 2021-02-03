using Infra.CrossCutting.Core.CQRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface ICategoryApplication
    {
        Task<IResult> GetCategories();
    }
}