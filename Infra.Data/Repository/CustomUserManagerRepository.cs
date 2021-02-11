using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Repository;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class CustomUserManagerRepository : ICustomUserManagerRepository
    {
        private readonly ServiceIdentityDbContext _serviceIdentityDbContext;

        public CustomUserManagerRepository(ServiceIdentityDbContext serviceIdentityDbContext)
        {
            _serviceIdentityDbContext = serviceIdentityDbContext;
        }

        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            var user = await _serviceIdentityDbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);

            return user; 
        }

        public string GetUserById(string email)
        {
            var user =  _serviceIdentityDbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);

            return user.Result.Id; 
        }
    }
}