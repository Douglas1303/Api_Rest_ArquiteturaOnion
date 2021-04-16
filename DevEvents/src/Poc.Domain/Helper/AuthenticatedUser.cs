using Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using Poc.Domain.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Poc.Domain.Helper
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string Email => _accessor.HttpContext.User.Identity.Name;
        private string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
        private string Id => _accessor.HttpContext.User.Identity.GetId().ToString(); 



        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public string EmailUser { get => Email; }

        public string UserId { get => Id; }

        //public static class Factory
        //{
        //    public static AuthenticatedUser GetIntance()
        //    {
        //        IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)Bootstrap.ServiceProvider.GetService(typeof(IHttpContextAccessor));
        //        AuthenticatedUser intance = new AuthenticatedUser(httpContextAccessor);

        //        return intance;
        //    }
        //}
    }
}