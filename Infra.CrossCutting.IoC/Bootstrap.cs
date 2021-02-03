using AutoMapper;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository;
using Infra.Data.Repository.Base;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Application.AutoMapper;
using Poc.Application.Interface;
using Poc.Application.Interface.Identity;
using Poc.Application.Service;
using Poc.Application.Service.Identity;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;

namespace Infra.CrossCutting.IoC
{
    public static class Bootstrap
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public static void RegisterService(IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatorHandler));
            services.AddTransient<IMediatorHandler, MediatorHandler>();

            //Context
            services.AddScoped<DevEventsDbContext>();

            //Application
            services.AddScoped<IAuthorizationApplication, AuthorizationApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();

            //Repository
            services.AddScoped<IDapperBase, DapperBase>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //Automapper
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            //Logger
            services.AddScoped<ILogModel, LogModel>(); 

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}