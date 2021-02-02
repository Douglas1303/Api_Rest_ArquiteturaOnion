using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Application.Interface.Identity;
using Poc.Application.Service.Identity;

namespace Infra.CrossCutting.IoC
{
    public static class Bootstrap
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public static void RegisterService(IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatorHandler));
            services.AddTransient<IMediatorHandler, MediatorHandler>();

            //Application
            services.AddScoped<IAuthorizationApplication, AuthorizationApplication>();

            //Logger
            services.AddScoped<ILogModel, LogModel>(); 

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}