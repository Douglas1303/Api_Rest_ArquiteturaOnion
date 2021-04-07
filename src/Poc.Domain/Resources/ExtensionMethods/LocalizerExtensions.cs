using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Poc.Domain.Resources.ExtensionMethods
{
    public static class LocalizerExtensions
    {
        public static string GetMsg<T>(this IStringLocalizer<T> localizer, string key)
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR", false);
            return localizer.GetString(key)?.Value;
        }
    }
}