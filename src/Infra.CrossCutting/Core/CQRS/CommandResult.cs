using FluentValidation.Results;
using System.Collections.Generic;

namespace Infra.CrossCutting.Core.CQRS
{
    public sealed class CommandResult : BaseResult
    {
        public CommandResult()
        {
        }

        public CommandResult(in IEnumerable<string> errors)
        {
            AddRange(errors);
        }

        public CommandResult(in ValidationResult result)
        {
            AddRange(result);
        }

        public CommandResult(string error)
        {
            AddError(error);
        }

        /// <summary>
        /// Utilize para adicionar mais mensagens de erro
        /// </summary>
        /// <param name="error"></param>
        public void AddErrorMessage(in string error)
        {
            AddError(error);
        }

        public static IResult Empty()
        {
            return new CommandResult();
        }
    }
}