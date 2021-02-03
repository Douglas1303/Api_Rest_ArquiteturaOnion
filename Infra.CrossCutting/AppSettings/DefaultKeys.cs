using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.AppSettings
{
    [ExcludeFromCodeCoverage]
    public static class DefaultKeys
    {
        public static string IdentityListo() => "DefaultConnection";
        public static string DevEvents_Domain() => "ConnectionDomain_DevEvents";
    }
}