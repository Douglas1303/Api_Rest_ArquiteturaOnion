using Infra.CrossCutting.Core.CQRS;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface ICepApplication
    {
        Task<IResult> GetCepAsync(string cep);
    }
}