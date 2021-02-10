using ExternalServices.Cep.Model;
using Refit;
using System.Threading.Tasks;

namespace ExternalServices.Cep.Interface
{
    public interface ICepService
    {
        [Get("/ws/{cep}/json")]
        Task<CepModel> GetAddressAsync(string cep);
    }
}