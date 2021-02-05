using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Poc.Domain.Entities;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(DevEventsDbContext context, IDapperBase dapper, ILogModel log) : base(context, dapper, log)
        {
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();

                return categories;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}