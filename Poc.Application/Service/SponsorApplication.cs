using AutoMapper;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Enum;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class SponsorApplication : ISponsorApplication
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IMediator _mediator;
        private readonly ILogModel _logModel;
        protected readonly IMapper _mapper;
        private readonly ICepService _cepService;
        private readonly IStringLocalizer<SponsorAppRsc> Localizer;

        #region Constantes
        private const string GetAllSponsorError = "GetAllSponsorError";
        private const string AddSponsorError = "AddSponsorError"; 
        private const string RemoveSponsorError = "RemoveSponsorError"; 
        #endregion

        public SponsorApplication(ISponsorRepository sponsorRepository, IMediator mediator, ILogModel logModel,
                                  IMapper mapper, ICepService cepService, IStringLocalizer<SponsorAppRsc> localizer)
        {
            _sponsorRepository = sponsorRepository;
            _mediator = mediator;
            _logModel = logModel;
            _mapper = mapper;
            _cepService = cepService;
            Localizer = localizer;
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<SponsorViewModel>>(await _sponsorRepository.GetAll()));
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(GetAllSponsorError)); 
                throw ex;
            }
        }

        public async Task<IResult> AddAsync(AddSponsorViewModel viewModel)
        {
            try
            {
                var address = await _cepService.GetAddressAsync(viewModel.Cep);

                var command = new AddSponsorCommand(
                    (ETipoPatrocinador)viewModel.TipoPatrocinador,
                    viewModel.NomePatrocinador,
                    viewModel.Documento,
                    viewModel.Telefone,
                    address.Cep,
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
                return new QueryResult(Localizer.GetMsg(AddSponsorError)); 
                throw ex;
            }
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            try
            {
                var command = new RemoveSponsorCommand(id);

                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return new QueryResult(Localizer.GetMsg(RemoveSponsorError)); 
                throw ex;
            }
        }
    }
}