using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DevEventsDbContext context, IDapperBase dapper, ILogModel log) : base(context, dapper, log)
        {
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            try
            {
                var users = await _context.Users.AsNoTracking().ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public void Add(UserModel userModel)
        {
            try
            {
               _context.Users.AddAsync(userModel);
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }

        public bool UserExists(string userName)
        {
            try
            {
                var count = _context.Users.Where(x => x.NomeCompleto == userName).Count();

                return count <= 0; 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }
    }
}