using Poc.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<SponsorDto>> GetAll();
    }
}