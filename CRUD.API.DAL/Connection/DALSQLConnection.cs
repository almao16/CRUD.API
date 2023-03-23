using CRUD.API.DAL.Container;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace CRUD.API.DAL.Connection
{
    internal class DALSQLConnection : IDALConnection<DALSQLConnection>
    {
        public DbConnection GetConnection()
        {
            DbConnection _connection = new SqlConnection(DALContainer._configuration.GetConnectionString("SQL"));
            return _connection;
        }
    }
}
