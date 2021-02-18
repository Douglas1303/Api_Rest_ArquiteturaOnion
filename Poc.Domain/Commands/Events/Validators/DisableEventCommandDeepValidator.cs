using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;

namespace Poc.Domain.Commands.Events.Validators
{
    internal class DisableEventCommandDeepValidator : BaseValidator<DisableEventCommand, DisableEventRsc>, IDeepValidator<DisableEventCommand>
    {
        private const string EventIdNotExistsError = "EventIdNotExistsError";
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
        }

        private bool CheckEventExists(int eventId)
        {
            return _eventRepository.EventIdExists(eventId);
        }
    }
}