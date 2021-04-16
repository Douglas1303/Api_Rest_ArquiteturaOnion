using FluentValidation;
using Infra.CrossCutting.Core.CQRS.Command;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Poc.Domain.Commands.BaseValidators
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseValidator<Cmd, Resx> : AbstractValidator<Cmd> where Cmd : Command where Resx : class
    {
        private IStringLocalizer<Resx> Localizer;

        protected BaseValidator(IStringLocalizer<Resx> localizer)
        {
            CascadeMode = CascadeMode.Stop;
            Localizer = localizer;
        }

        //TODO: Isso será feito na applicação no contexto geral, criado apenas para teste, acabou ficando eqto n temos o token
        protected string GetMessage(string key)
        {
            SetCurrentCulture();
            return Localizer.GetString(key)?.Value;
        }

        private void SetCurrentCulture()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR", false);//TODO: Solução temporária.
        }
    }
}