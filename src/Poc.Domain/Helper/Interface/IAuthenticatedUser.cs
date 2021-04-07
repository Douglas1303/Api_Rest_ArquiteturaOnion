using System.Collections.Generic;
using System.Security.Claims;

namespace Poc.Domain.Helper.Interface
{
    public interface IAuthenticatedUser
    {
        IEnumerable<Claim> GetClaimsIdentity();
        string EmailUser { get; }
        string UserId { get; }
    }
}