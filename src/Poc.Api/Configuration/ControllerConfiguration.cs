using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Poc.Domain.Entities.Validations;
using System.Globalization;

namespace Poc.Api.Configuration
{
    public static class ControllerConfiguration
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = true;
                })
             .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
             .AddFluentValidation(p => {
                 p.RegisterValidatorsFromAssemblyContaining<CategoryModelValidator>();
                 p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
             })
             .AddDataAnnotationsLocalization();
        }
    }
}