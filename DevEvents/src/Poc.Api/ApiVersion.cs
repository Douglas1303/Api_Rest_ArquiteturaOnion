﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Poc.Api
{
    [ExcludeFromCodeCoverage]
    public static class ApiVersion
    {
        public static string Get()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.ProductVersion;
        }
    }
}