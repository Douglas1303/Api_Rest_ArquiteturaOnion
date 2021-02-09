using Poc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllAsync();

        void AddAsync(UserModel userModel);
    }
}