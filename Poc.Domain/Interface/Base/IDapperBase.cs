using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Domain.Interface.Base
{
    public interface IDapperBase
    {
        void ExecuteProcedure(string databaseKey, string name);

        void ExecuteProcedure(string databaseKey, string name, object parameters);

        IEnumerable<T> ExecuteProcedure<T>(string databaseKey, string name);

        IEnumerable<T> ExecuteProcedure<T>(string databaseKey, string name, object parameters);

        Task<IEnumerable<T>> ExecuteProcedureAsync<T>(string databaseKey, string name, object parameters);

        IEnumerable<T> GetAll<T>(string databaseKey, string tableName);

        IEnumerable<T> Get<T>(string databaseKey, string tableName, object parameters);

        IEnumerable<T> Get<T>(string databaseKey, object parameters);

        IEnumerable<T> Query<T>(string databaseKey, string query);

        IEnumerable<T> GetExecuteQuery<T>(string databaseKey, string query);

        T ExecuteProcedureScalar<T>(string databaseKey, string name, object parameters);

        Task<T> ExecuteProcedureScalarAsync<T>(string databaseKey, string procedureName, DynamicParameters parameters);

        T ExecuteProcedureParam<T>(string databaseKey, string name, object parameters);

        Task<T> ExecuteScalarAsync<T>(string databaseKey, string query);

        T ExecuteScalar<T>(string databaseKey, string query);

        Task<T> ExecuteProcedureScalarAsync<T>(string databaseKey, string name, object parameters);

        Task<T> ExecuteProcedureScalarFirstAsync<T>(string databaseKey, string procedureName, DynamicParameters parameters);

        void ExecuteScalarOutputParam<T>(string databaseKey, string name, DynamicParameters parameters, string outputParamName, out T result);

        Task<T> ExecuteProcedureFirstOrDefaultAsync<T>(string databaseKey, string name, object parameters);
    }
}