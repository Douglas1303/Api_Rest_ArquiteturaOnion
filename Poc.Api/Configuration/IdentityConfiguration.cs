using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Poc.Api.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentitytConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ServiceIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}