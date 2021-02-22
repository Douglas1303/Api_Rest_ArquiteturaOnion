using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.Commands.Sponsor.Validators
{
    internal class RemoveSponsorCommandDeepValidator : BaseValidator<RemoveSponsorCommand, RemoveSponsorRsc>, IDeepValidator<RemoveSponsorCommand>
    {
        private const string SponsorNotExists = "SponsorNotExists";
        private readonly ISponsorRepository _sponsorRepository; 
        public RemoveSponsorCommandDeepValidator(ISponsorRepository sponsorRepository, IStringLocalizer<RemoveSponsorRsc> localizer) : base(localizer)
        {
            _sponsorRepository = sponsorRepository;
            Validations(); 
        }

        private void Validations()
        {
            RuleFor(x => x)
                .MustAsync(SponsorExists)
                .WithMessage(x => GetMessage(SponsorNotExists))
                .WithErrorCode(SponsorNotExists); 
        }

        private async Task<bool> SponsorExists(RemoveSponsorCommand cmd, CancellationToken arg2)
        {
            var sponsor = await _sponsorRepository.SponsorExists(cmd.Id);

            return sponsor != null; 
        }
    }
}