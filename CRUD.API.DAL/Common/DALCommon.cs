using Autofac;
using CRUD.API.DAL.Connection;
using CRUD.API.DAL.Container;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.DAL.Common
{
    public class DALCommon : IDALCommon
    {
        public async Task<T> GetAsync<T>(string storedProcedure, object parammeters) where T : new()
        {
            DbConnection _connection = null;
            T retValue = new T();
            try
            {
                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IDALConnection<DALSQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = await _connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<IList<T>> GetListAsync<T>(string storedProcedure, object parammeters)
        {
            DbConnection _connection = null;
            List<T> retValue = new List<T>();
            try
            {
                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IDALConnection<DALSQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = (List<T>)await _connection.QueryAsync<T>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                    if (retValue != null && retValue.Count == 0)
                    {
                        retValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<bool> ExecuteAsync(string storedProcedure, object parammeters)
        {
            DbConnection _connection = null;
            bool retValue = false;
            try
            {
                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IDALConnection<DALSQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    if (await _connection.ExecuteAsync(storedProcedure, parammeters, commandType: CommandType.StoredProcedure) > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<string> ExecuteScalarAsync(string storedProcedure, object parammeters)
        {
            DbConnection _connection = null;
            string retValue = string.Empty;
            try
            {
                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IDALConnection<DALSQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = await _connection.QueryFirstOrDefaultAsync<string>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue ?? string.Empty;
        }
    }
}
