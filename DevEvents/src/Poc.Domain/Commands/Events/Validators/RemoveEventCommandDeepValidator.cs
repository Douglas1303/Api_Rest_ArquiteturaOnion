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
    internal class RemoveEventCommandDeepValidator : BaseValidator<RemoveEventCommand, RemoveEventRsc>, IDeepValidator<RemoveEventCommand>
    {
        private const string EventIdNotExistsError = "EventIdNotExistsError";
        private readonly IEventRepository _eventRepository;

        public RemoveEventCommandDeepValidator(IStringLocalizer<RemoveEventRsc> localizer, IEventRepository eventRepository) : base(localizer)
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