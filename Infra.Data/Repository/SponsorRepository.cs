using Infra.CrossCutting.AppSettings;
using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class SponsorRepository : BaseRepository, ISponsorRepository
    {
        public SponsorRepository(DevEventsDbContext context, IDapperBase dapper, ILogModel log) : base(context, dapper, log)
        {
        }

        public async Task<IEnumerable<SponsorDto>> GetAll()
        {
            try
            {
                var sponsors = await _dapper.ExecuteProcedureAsync<SponsorDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Sps_Sponsor]", null);

                return sponsors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}