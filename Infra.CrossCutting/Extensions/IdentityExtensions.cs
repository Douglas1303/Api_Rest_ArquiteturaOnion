using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Infra.CrossCutting.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.Sid);

            return claim?.Value ?? string.Empty;
        }

        public static string GetEmailUser(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Email);

            return claim?.Value ?? string.Empty;
        }

        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetClaims(this IIdentity Identity, string claimType)
        {
            ClaimsIdentity claimsIdentity = Identity as ClaimsIdentity;

            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}