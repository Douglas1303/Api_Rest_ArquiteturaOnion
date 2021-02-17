using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Core.CQRS.Command;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Mediator
{
    public interface IMediatorHandler
    {
        Task<IResult> SendCommand<T>(T command) where T : Command;
    }
}