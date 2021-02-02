using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class ServiceIdentityDbContext : IdentityDbContext
    {
        public ServiceIdentityDbContext(DbContextOptions<ServiceIdentityDbContext> options) : base(options)
        {
        }

        public ServiceIdentityDbContext()
        {

        }
    }
}