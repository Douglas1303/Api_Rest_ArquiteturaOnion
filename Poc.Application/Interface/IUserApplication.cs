using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface IUserApplication
    {
        Task<IResult> GetAllAsync();

        Task<IResult> AddAsync(AddUserViewModel addUserViewModel);
    }
}