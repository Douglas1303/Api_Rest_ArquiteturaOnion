using ExternalServices.Cep.Interface;
using Infra.CrossCutting.Core.CQRS;
using Poc.Application.Interface;
using Refit;
using System;
using System.Threading.Tasks;

namespace Poc.Application.Service
{
    public class CepApplication : ICepApplication
    {
        private readonly ICepService _cepService;

        public CepApplication(ICepService cepService)
        {
            _cepService = cepService;
        }

        public async Task<IResult> GetCepAsync(string cep)
        {
            try
            {
                var address = await _cepService.GetAddressAsync(cep);

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