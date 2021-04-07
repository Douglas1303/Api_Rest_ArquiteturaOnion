using Microsoft.AspNetCore.Http;
using Poc.Domain.Helper.Interface;
using System.IO;
using System.Security.Claims;

namespace Poc.Domain.Helper
{
    public class UserInfo : IUserInfo
    {
        private readonly int _userId;

        public UserInfo()
        {
        }

        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            Claim nameIdentifier = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Claim sid = httpContextAccessor.HttpContext.User.FindFirst(Path.GetFileName(ClaimTypes.Sid));

            if (nameIdentifier == null || sid == null ||
                string.IsNullOrEmpty(sid.Value) ||
                string.IsNullOrEmpty(nameIdentifier.Value))

                return;

            string[] nameIdentifierArray = nameIdentifier.Value.Split('@');

            if (nameIdentifierArray.Length < 2)
                return;

            int.TryParse(httpContextAccessor.HttpContext.User.FindFirst(Path.GetFileName(ClaimTypes.Sid)).Value, out _userId);
            UserName = nameIdentifierArray[0];
        }

        public int UserId { get => _userId; }

        public string UserName { get; private set; }
    }
}