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
using System.Data;
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

        public async Task<int> AddAsync(SponsorDto dto)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", 0, direction: ParameterDirection.Output);
                parameters.Add("@NomePatrocinador", dto.NomePatrocinador); 
                parameters.Add("@Documento", dto.Documento); 
                parameters.Add("@Cep", dto.Cep); 
                parameters.Add("@Logradouro", dto.Logradouro); 
                parameters.Add("@Complemento", dto.Complemento); 
                parameters.Add("@Bairro", dto.Bairro); 
                parameters.Add("@Localidade", dto.Localidade); 
                parameters.Add("@UF", dto.UF); 
                parameters.Add("@DDD", dto.DDD);
                parameters.Add("@DataCadastro", dto.DataCadastro); 

                var result = await _dapper.ExecuteProcedureScalarAsync<SponsorDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Spi_Sponsor]", parameters);
               
                //if (result.Result == -1)
                //    return 0;

                return result.Id; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> RemoveAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                var result = await _dapper.ExecuteProcedureScalarAsync<ProcedureResultDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Spu_Sponsor]", parameters);

                return result.ErrorMessage; 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SponsorDto> SponsorExists(int id)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Id", id);

                return await _dapper.ExecuteProcedureScalarAsync<SponsorDto>(DefaultKeys.DevEvents_Domain(), "[dbo].[Spu_SponsorById]", parameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}