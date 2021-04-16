using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Tools.Service
{
    public interface IRsaService
    {
        Task<RSAParameters> GetRSAParameters();
    }
}