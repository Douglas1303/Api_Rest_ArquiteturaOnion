using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Infra.CrossCutting.AppSettings
{
    [ExcludeFromCodeCoverage]
    public static class GetAppSetting
    {
        public static string GetConnection(string key)
        {
            var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
          .Build();
            return configuration.GetConnectionString(key);
        }
    }
}