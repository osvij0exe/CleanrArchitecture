using CleanArchitecture.Application.Abstractions.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly string _ConnectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public IDbConnection CreateConnecton()
        {
            //coneccion a base de datos postgrest
            var connection = new NpgsqlConnection(_ConnectionString);
            connection.Open();

            return connection;

        }




    }
}
