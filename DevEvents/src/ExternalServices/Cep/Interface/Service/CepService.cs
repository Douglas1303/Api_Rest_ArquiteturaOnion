using ExternalServices.Cep.Model;
using Refit;
using System.Threading.Tasks;

namespace ExternalServices.Cep.Interface.Service
{
    public class CepService : ICepService
    {
        public async Task<CepModel> GetAddressAsync(string cep)
        {
            var cepClient = RestService.For<ICepService>("http://viacep.com.br");

            var address = await cepClient.GetAddressAsync(cep);

            return address;
        }
    }
}