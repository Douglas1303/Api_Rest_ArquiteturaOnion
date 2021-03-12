using AutoMapper;
using ExternalServices.Cep;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Infra.CrossCutting.Models;
using Microsoft.Extensions.Localization;
using Poc.Application.Interface;
using Poc.Domain.Resources.Application;
using Poc.Domain.Resources.ExtensionMethods;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class CepApplication : ICepApplication
    {
        private readonly ICepService _cepService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CepAppRsc> Localizer;
        private readonly ILogModel _log;

        #region Constantes
        private const string GetCepError = "GetCepError";
        #endregion

        public CepApplication(ICepService cepService, IMapper mapper, IStringLocalizer<CepAppRsc> localizer, ILogModel log)
        {
            _cepService = cepService;
            _mapper = mapper;
            Localizer = localizer;
            _log = log;
        }

        public async Task<IResult> GetCepAsync(string cep)
        {
            try
            {
                var address = _mapper.Map<CepViewModel>(await _cepService.GetAddressAsync(cep));

                if (address.Cep == null) return new QueryResult("Endereço não encontrado.");

                return new QueryResult(address);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                return new QueryResult(Localizer.GetMsg(GetCepError)); 
            }
        }
    }
}