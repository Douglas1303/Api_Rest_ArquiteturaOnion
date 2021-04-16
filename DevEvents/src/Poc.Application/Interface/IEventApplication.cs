using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface IEventApplication
    {
        Task<IResult> GetAllAsync();

        Task<IResult> GetByIdAsync(int eventId);

        Task<IResult> AddAsync(AddEventViewModel eventViewModel);

        Task<IResult> UpdateAsync(UpdateEventViewModel eventViewModel);

        Task<IResult> RegisterAsync(int eventId);

        Task<IResult> CancelAsync(int eventId);
        Task<IResult> RemoveAsync(int eventId);
    }
}