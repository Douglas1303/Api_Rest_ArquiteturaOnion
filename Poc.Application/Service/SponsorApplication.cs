﻿using AutoMapper;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using MediatR;
using Poc.Application.Interface;
using Poc.Application.ViewModel;
using Poc.Domain.Commands.Sponsor;
using Poc.Domain.Enum;
using Poc.Domain.Interface.Repository;
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

        public SponsorApplication(ISponsorRepository sponsorRepository, IMediator mediator, ILogModel logModel, IMapper mapper, ICepService cepService)
        {
            _sponsorRepository = sponsorRepository;
            _mediator = mediator;
            _logModel = logModel;
            _mapper = mapper;
            _cepService = cepService;
        }

        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return new QueryResult(_mapper.Map<IEnumerable<SponsorViewModel>>(await _sponsorRepository.GetAll()));
            }
            catch (Exception ex)
            {
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
                throw ex;
            }
        }
    }
}