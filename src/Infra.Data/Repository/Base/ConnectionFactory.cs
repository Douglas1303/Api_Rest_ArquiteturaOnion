using Infra.CrossCutting.AppSettings;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.Repository.Base
{
    [ExcludeFromCodeCoverage]
    public static class ConnectionFactory
    {
        public static IDbConnection Connection(string databaseKey, string connectionString)
        {
            if (databaseKey == DefaultKeys.Identity())
                return new SqlConnection(connectionString);

            if (databaseKey == DefaultKeys.DevEvents_Domain())
                return new SqlConnection(connectionString);

            throw new Exception($"Conneciton not found: {databaseKey} - {connectionString}");
        }
    }
}