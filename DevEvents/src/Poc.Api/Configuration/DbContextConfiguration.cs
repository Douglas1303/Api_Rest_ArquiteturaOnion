using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Poc.Api.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ServiceIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DevEventsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionDomain_DevEvents")));


            return services; 
        }
    }
}