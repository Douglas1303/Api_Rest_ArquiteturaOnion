using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]
namespace Poc.Domain.Commands.Events.Validators
{
    internal class RegisterEventUserCommandDeepValidator : BaseValidator<RegisterEventUserCommand, RegisterEventUserRsc>, IDeepValidator<RegisterEventUserCommand>
    {
        private const string EventIdNotExistsError = "EventIdNotExistsError";
        private const string UserIdNotExistsError = "UserIdNotExistsError";

        private readonly IEventRepository _eventRepository;

        public RegisterEventUserCommandDeepValidator(IStringLocalizer<RegisterEventUserRsc> localizer, IEventRepository eventRepository) : base(localizer)
        {
            _eventRepository = eventRepository;
            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.EventoId)
                .Must(CheckEventExists)
                .WithErrorCode(EventIdNotExistsError)
                .WithMessage(x => GetMessage(EventIdNotExistsError));

            RuleFor(x => x.UsuarioId)
                .Must(CheckUserExists)
                .WithErrorCode(UserIdNotExistsError)
                .WithMessage(x => GetMessage(UserIdNotExistsError));
        }

        private bool CheckEventExists(int eventId)
        {
            return _eventRepository.EventIdExists(eventId);
        }

        private bool CheckUserExists(int userId)
        {
            return _eventRepository.UserIdExists(userId);
        }
    }
}