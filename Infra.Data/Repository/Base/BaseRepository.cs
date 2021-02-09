using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Poc.Domain.Interface.Base;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.Repository.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository
    {
        protected readonly DevEventsDbContext _context;
        protected readonly IDapperBase _dapper;
        protected readonly ILogModel _log;

        protected BaseRepository(DevEventsDbContext context, IDapperBase dapper, ILogModel log)
        {
            _context = context;
            _dapper = dapper;
            _log = log;
        }
    }
}