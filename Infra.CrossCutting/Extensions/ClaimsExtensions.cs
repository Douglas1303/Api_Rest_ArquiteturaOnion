using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Infra.CrossCutting.Extensions
{
    public static class ClaimsExtensions
    {
        public static IList<Claim> GetRoles(this ClaimsPrincipal claims)
        {
            var list = ((ClaimsIdentity)claims.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type.Equals("AllowedAction"))
                .Select(c => c).ToList();

            return list; 
        }
    }
}