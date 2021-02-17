using AutoMapper;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.IoC.PipelineBehavior;
using Infra.CrossCutting.Mediator;
using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository;
using Infra.Data.Repository.Base;
using Infra.Data.UnitOfWork;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Poc.Application.AutoMapper;
using Poc.Application.Interface;
using Poc.Application.Interface.Identity;
using Poc.Application.PipelineBehavior;
using Poc.Application.Service;
using Poc.Application.Service.Identity;
using Poc.Domain.CommandHandlers.Events;
using Poc.Domain.Commands.Events;
using Poc.Domain.Helper;
using Poc.Domain.Helper.Interface;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;

namespace Infra.CrossCutting.IoC
{
    public static class Bootstrap
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public static void RegisterService(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserInfo>(a => new UserInfo(a.GetRequiredService<IHttpContextAccessor>()));

            services.AddMediatR(typeof(MediatorHandler));
            services.AddTransient<IMediatorHandler, MediatorHandler>();

            //Context
            services.AddScoped<DevEventsDbContext>();

            //Application
            services.AddScoped<IAuthorizationApplication, AuthorizationApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IEventApplication, EventApplication>();

            //Repository
            services.AddScoped<IDapperBase, DapperBase>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ICustomUserManagerRepository, CustomUserManagerRepository>();

            //Automapper
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            //Command Handlers
            services.AddTransient<IRequestHandler<AddEventCommand, IResult>, AddEventCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateEventCommand, IResult>, UpdateEventCommandHandler>();
            services.AddTransient<IRequestHandler<RegisterEventUserCommand, IResult>, RegisterEventUserCommandHandler>();
            services.AddTransient<IRequestHandler<DisableEventCommand, IResult>, DisableEventCommandHandler>();
            services.AddTransient<IRequestHandler<RemoveEventCommand, IResult>, RemoveEventCommandHandler>();

            //Logger
            services.AddScoped<ILogModel, LogModel>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddPipelineBehaviorValidation();

            services.AddScoped(typeof(IRequestExceptionHandler<AddEventCommand, IResult, Exception>), typeof(HandlerExceptionBehavior));
            services.AddScoped(typeof(IRequestExceptionHandler<DisableEventCommand, IResult, Exception>), typeof(HandlerExceptionBehavior));
            services.AddScoped(typeof(IRequestExceptionHandler<RegisterEventUserCommand, IResult, Exception>), typeof(HandlerExceptionBehavior));
            services.AddScoped(typeof(IRequestExceptionHandler<UpdateEventCommand, IResult, Exception>), typeof(HandlerExceptionBehavior));
            services.AddScoped(typeof(IRequestExceptionHandler<RemoveEventCommand, IResult, Exception>), typeof(HandlerExceptionBehavior));

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}