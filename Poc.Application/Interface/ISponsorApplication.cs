using Infra.CrossCutting.Core.CQRS;
using System.Threading.Tasks;

namespace Poc.Application.Interface
{
    public interface ISponsorApplication
    {
        Task<IResult> GetAll(); 
    }
}