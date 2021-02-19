using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using MediatR;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Sponsor;
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

        public async Task<IResult> AddAsync(AddSponsorViewModel viewModel)
        {
            try
            {
                var command = new AddSponsorCommand(
                    viewModel.NomePatrocinador, 
                    viewModel.Documento, 
                    viewModel.Cep,
                    viewModel.Logradouro, 
                    viewModel.Complemento, 
                    viewModel.Bairro, 
                    viewModel.Localidade, 
                    viewModel.UF, 
                    viewModel.DDD
                    );

                return await _mediator.Send(command); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IResult> GetAllAsync()
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