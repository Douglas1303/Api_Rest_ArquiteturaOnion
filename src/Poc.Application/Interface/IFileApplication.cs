using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface IFileApplication
    {
        Task<IResult> AddAsync(AddFileViewModel viewModel);
    }
}