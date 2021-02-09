using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.AppSettings
{
    [ExcludeFromCodeCoverage]
    public static class DefaultKeys
    {
        public static string Identity() => "DefaultConnection";
        public static string DevEvents_Domain() => "ConnectionDomain_DevEvents";
    }
}