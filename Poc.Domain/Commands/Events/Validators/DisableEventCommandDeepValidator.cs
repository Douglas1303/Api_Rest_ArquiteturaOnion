using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Poc.Test")]
namespace Poc.Domain.Commands.Events.Validators
{
    internal class DisableEventCommandDeepValidator : BaseValidator<DisableEventCommand, DisableEventRsc>, IDeepValidator<DisableEventCommand>
    {
        private const string EventIdNotExistsError = "EventIdNotExistsError";
        private const string StatusIsFalseError = "StatusIsFalseError";
        private readonly IEventRepository _eventRepository;

        public DisableEventCommandDeepValidator(IStringLocalizer<DisableEventRsc> localizer, IEventRepository eventRepository) : base(localizer)
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

            RuleFor(x => x.EventoId)
                .Must(CheckStatusIsFalse)
                .WithErrorCode(StatusIsFalseError)
                .WithMessage(x => GetMessage(StatusIsFalseError));
        }

        private bool CheckEventExists(int eventId)
        {
            return _eventRepository.EventIdExists(eventId);
        }

        private bool CheckStatusIsFalse(int id)
        {
            return _eventRepository.StatusIsFalse(id); 
        }
    }
}