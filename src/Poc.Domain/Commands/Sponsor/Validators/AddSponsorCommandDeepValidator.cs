using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using System;

namespace Poc.Domain.Commands.Sponsor.Validators
{
    public class AddSponsorCommandDeepValidator : BaseValidator<AddSponsorCommand, AddSponsorRsc>, IDeepValidator<AddSponsorCommand>
    {
        private readonly ISponsorRepository _sponsorRepository;
        private const string NamesSponsorExistsError = "NamesSponsorExistsError";

        public AddSponsorCommandDeepValidator(ISponsorRepository sponsorRepository, IStringLocalizer<AddSponsorRsc> localizer) : base(localizer)
        {
            _sponsorRepository = sponsorRepository;
            Validatons();
        }

        private void Validatons()
        {
            RuleFor(x => x.NomePatrocinador)
                .Must(CheckNameSponsor)
                .WithMessage(x => GetMessage(NamesSponsorExistsError))
                .WithErrorCode(NamesSponsorExistsError);
        }

        private bool CheckNameSponsor(string name)
        {
            return _sponsorRepository.NameSponsorExists(name);
        }
    }
}