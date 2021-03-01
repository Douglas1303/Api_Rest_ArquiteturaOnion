using AutoMapper;
using ExternalServices.Cep;
using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Poc.Application.Interface;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class CepApplication : ICepApplication
    {
        private readonly ICepService _cepService;
        private readonly IMapper _mapper;

        public CepApplication(ICepService cepService, IMapper mapper)
        {
            _cepService = cepService;
            _mapper = mapper;
        }

        public async Task<IResult> GetCepAsync(string cep)
        {
            try
            {
                var address = _mapper.Map<CepViewModel>(await _cepService.GetAddressAsync(cep));

                if (address.Cep == null) return new QueryResult("Endereço não encontrado");

                return new QueryResult(address);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}