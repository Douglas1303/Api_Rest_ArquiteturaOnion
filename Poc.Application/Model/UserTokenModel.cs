using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Poc.Application.Model
{
    public class UserTokenModel
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public string Message { get; set; }
    }
}