using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface ISponsorApplication
    {
        Task<IResult> GetAllAsync();
        Task<IResult> AddAsync(AddSponsorViewModel viewModel);
        Task<IResult> RemoveAsync(int id); 
    }
}