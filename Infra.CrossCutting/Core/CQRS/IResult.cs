using System.Collections.Generic;

namespace Infra.CrossCutting.Core.CQRS
{
    public interface IResult
    {
        IList<string> Messages { get; }
        StatusResult Status { get; }
        object Data { get; set; }
    }
}