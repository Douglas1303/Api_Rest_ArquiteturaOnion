using Microsoft.AspNetCore.Identity;
using Poc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface ICustomUserManagerRepository
    {
        Task<IdentityUser> GetUserByEmail(string email);
        string GetUserById(string email);
    }
}
