using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using Poc.Domain.Resources.CommandHandler;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Sponsor
{
    public class RemoveSponsorCommandHandler : IRequestHandler<RemoveSponsorCommand, IResult>
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<RemoveSponsorCommandHandlerRsc> Localizer;

        private const string RemoveSponsorError = "RemoveSponsorError"; 

        public RemoveSponsorCommandHandler(ISponsorRepository sponsorRepository, IUnitOfWork unitOfWork, IStringLocalizer<RemoveSponsorCommandHandlerRsc> localizer)
        {
            _sponsorRepository = sponsorRepository;
            _unitOfWork = unitOfWork;
            Localizer = localizer; 
        }

        public async Task<IResult> Handle(RemoveSponsorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sponsorRepository.RemoveAsync(request.Id); 

                await _unitOfWork.Commit(); 
            }
            catch (Exception)
            {
                var cmdResult = new CommandResult();
                cmdResult.AddErrorMessage(Localizer.GetMsg(RemoveSponsorError));

                return cmdResult; 
            }

            return CommandResult.Empty(); 
        }
    }
}