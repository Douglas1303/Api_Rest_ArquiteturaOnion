using Poc.Domain.Dtos;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface IFileRepository
    {
        Task<int> AddAsync(FileDto fileDto);
    }
}