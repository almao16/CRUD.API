using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CRUD.API.DAL.Connection
{
    internal interface IDALConnection<T> where T : class
    {
        public DbConnection GetConnection();
    }
}
