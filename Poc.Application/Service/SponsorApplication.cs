using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using MediatR;
using Poc.Application.Interface;
using Poc.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class SponsorApplication : ISponsorApplication
    {
        private readonly ISponsorRepository _sponsorRepository; 
        private readonly IMediator _mediator;
        private readonly ILogModel _logModel;

        public SponsorApplication(ISponsorRepository sponsorRepository, IMediator mediator, ILogModel logModel)
        {
            _sponsorRepository = sponsorRepository; 
            _mediator = mediator;
            _logModel = logModel;
        }

        public async Task<IResult> GetAll()
        {
            try
            {
                return new QueryResult(await _sponsorRepository.GetAll()); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}