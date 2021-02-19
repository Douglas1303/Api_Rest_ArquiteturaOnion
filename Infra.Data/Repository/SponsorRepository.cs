using Dapper;
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
                var result = await _dapper.ExecuteProcedureAsync<SponsorDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Sps_Sponsor]", null);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(SponsorDto dto)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@NomePatrocinador", dto.NomePatrocinador); 
                parameters.Add("@Documento", dto.Documento); 
                parameters.Add("@Cep", dto.Cep); 
                parameters.Add("@Logradouro", dto.Logradouro); 
                parameters.Add("@Complemento", dto.Complemento); 
                parameters.Add("@Bairro", dto.Bairro); 
                parameters.Add("@Localidade", dto.Localidade); 
                parameters.Add("@UF", dto.UF); 
                parameters.Add("@DDD", dto.DDD);

               var result = _dapper.ExecuteProcedureScalar<SponsorDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Spi_Sponsor]", parameters);

                return result.Id; 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}