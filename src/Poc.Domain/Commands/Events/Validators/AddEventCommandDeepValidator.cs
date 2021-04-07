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
    internal class AddEventCommandDeepValidator : BaseValidator<AddEventCommand, AddEventRsc>, IDeepValidator<AddEventCommand>
    {
        private const string TitleExists = "TitleExists"; 
        private readonly IEventRepository _eventRepository;

        public AddEventCommandDeepValidator(IStringLocalizer<AddEventRsc> localizer, IEventRepository eventRepository) : base(localizer)
        {
            _eventRepository = eventRepository;
            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Titulo)
                .Must(CheckEventExists)
                .WithErrorCode(TitleExists)
                .WithMessage(x => GetMessage(TitleExists));
        }

        private bool CheckEventExists(string titulo)
        {
            return _eventRepository.TitleExists(titulo);
        }
    }
}