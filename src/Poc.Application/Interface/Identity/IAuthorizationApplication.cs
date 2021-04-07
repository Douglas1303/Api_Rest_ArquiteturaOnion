using Infra.CrossCutting.Core.CQRS;
using Poc.Application.ViewModel.Identity;
using System.Threading.Tasks;

namespace Poc.Application.Interface.Identity
{
    public interface IAuthorizationApplication
    {
        Task<IResult> RegisterUserAsync(UserIdentityViewModel userViewModel);
        Task<IResult> LoginAsync(LoginIdentityViewModel loginViewModel);
    }
}