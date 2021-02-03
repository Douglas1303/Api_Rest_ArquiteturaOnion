using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Poc.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //   Host.CreateDefaultBuilder(args)
        //       .ConfigureAppConfiguration((hostingContext, config) =>
        //       {
        //           config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //           .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        //       })
        //       .UseSerilog((context, config) =>
        //       {
        //           config.ReadFrom.Configuration(context.Configuration);
        //       })
        //       .ConfigureWebHostDefaults(webBuilder =>
        //       {
        //           webBuilder.UseStartup<Startup>();
        //       });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}