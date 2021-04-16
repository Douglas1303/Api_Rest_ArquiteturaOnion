using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Resources;

namespace Poc.Domain.Commands.Events.Validators
{
    internal class RegisterEventUserCommandShallowValidator : BaseValidator<RegisterEventUserCommand, RegisterEventUserRsc>, IShallowValidator<RegisterEventUserCommand>
    {
        private const string EventIdInvalidError = "EventIdInvalidError";
        private const string UserIdNotInvalidError = "UserIdNotInvalidError";

        public RegisterEventUserCommandShallowValidator(IStringLocalizer<RegisterEventUserRsc> localizer) : base(localizer)
        {
            RuleFor(c => c.EventId)
                .NotNull()
                .GreaterThan(0)
               .WithMessage(x => GetMessage(EventIdInvalidError))
               .WithErrorCode(EventIdInvalidError);

            RuleFor(c => c.UserId)
                .NotEmpty()
               .WithMessage(x => GetMessage(UserIdNotInvalidError))
               .WithErrorCode(UserIdNotInvalidError);
        }
    }
}