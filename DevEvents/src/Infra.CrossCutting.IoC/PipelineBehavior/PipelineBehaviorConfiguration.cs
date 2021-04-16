using Infra.CrossCutting.Core.IPipelineBehavior;
using Infra.CrossCutting.Mediator;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC.PipelineBehavior
{
    public static class PipelineBehaviorConfiguration
    {
        public static void AddPipelineBehaviorValidation(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatorHandler));

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.Scan(
               scan => scan.FromApplicationDependencies().AddClasses(@class => @class.AssignableTo(typeof(IShallowValidator<>))).AsImplementedInterfaces())
               .AddScoped(typeof(IPipelineBehavior<,>), typeof(ShallowValidatorBehavior<,>));

            services.Scan(
                scan => scan.FromApplicationDependencies().AddClasses(@class => @class.AssignableTo(typeof(IDeepValidator<>))).AsImplementedInterfaces())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(DeepValidatorBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
        }
    }
}