using Dapper;
using Infra.CrossCutting.AppSettings;
using Infra.CrossCutting.Models;
using Infra.Data.Context;
using Infra.Data.Repository.Base;
using Poc.Domain;
using Poc.Domain.Dtos;
using Poc.Domain.Interface.Base;
using Poc.Domain.Interface.Repository;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        public FileRepository(DevEventsDbContext context, IDapperBase dapper, ILogModel log) 
                                 : base(context, dapper, log)
        {
        }

        public async Task<int> AddAsync(FileDto fileDto)
        {
            try
            {
                var fileId = 0;

                var parameters = new DynamicParameters();

                parameters.Add("@Id", fileId, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TipoArquivoId", fileDto.TipoArquivoId);
                parameters.Add("@NomeDeOrigem", fileDto.NomeDeOrigem);
                parameters.Add("@NomeParaSalvar", fileDto.NomeDeOrigem);
                parameters.Add("@Ativo", fileDto.Ativo);

                var result = await _dapper.ExecuteProcedureScalarAsync<ProcedureResultModel>(DefaultKeys.DevEvents_Domain(), "[dbo].[Spi_File]", parameters);

                fileId = parameters.Get<int>("@Id");

                return fileId; 
            }
            catch (Exception ex)
            {
                _log.RecLog(ex);
                throw;
            }
        }
    }
}