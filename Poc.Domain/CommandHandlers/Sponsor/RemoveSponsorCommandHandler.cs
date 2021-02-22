using Infra.CrossCutting.Core.CQRS;
using MediatR;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Interface.Repository.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poc.Domain.CommandHandlers.Sponsor
{
    public class RemoveSponsorCommandHandler : IRequestHandler<RemoveSponsorCommand, IResult>
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveSponsorCommandHandler(ISponsorRepository sponsorRepository, IUnitOfWork unitOfWork)
        {
            _sponsorRepository = sponsorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(RemoveSponsorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sponsorRepository.RemoveAsync(request.Id); 

                await _unitOfWork.Commit(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CommandResult.Empty(); 
        }
    }
}