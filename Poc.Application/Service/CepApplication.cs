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
        public async Task<IResult> GetCepAsync(string cep)
        {
            try
            {
                var cepClient = RestService.For<ICepService>("http://viacep.com.br");

                var address = await cepClient.GetAddressAsync(cep);

                return new QueryResult(address);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}