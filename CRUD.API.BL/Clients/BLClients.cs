using Autofac;
using CRUD.API.BL.Utils;
using CRUD.API.DAL.Common;
using CRUD.API.DAL.Container;
using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Clients
{

    public class BLClients : IBLClients
    {
        public async Task<string> CreateAsync(ClientEntity clients)
        {
            string retValue = string.Empty;
            try
            {
                object parammeters = new
                {
                    clients.DniClient,
                    clients.Names,
                    clients.LastNames,
                    clients.Email,
                    
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteScalarAsync(ConstantsProcedures.CLIENTS_CREATE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> DeleteAsync(int IdClient)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdClient
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.CLIENTS_DELETE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<ClientEntity> GetAsync(int IdClient)
        {
            ClientEntity retValue = null;
            try
            {
                object parammeters = new
                {
                    IdClient
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().GetAsync<ClientEntity>(ConstantsProcedures.CLIENTS_GET, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<IList<ClientsEntity>> GetListAsync(int Page, int Rows, string Name)
        {
            List<ClientsEntity> retValue = null;
            try
            {
                object parammeters = new
                {
                    Page,
                    Rows,
                    Name
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = (List<ClientsEntity>)await scope.Resolve<IDALCommon>().GetListAsync<ClientsEntity>(ConstantsProcedures.CLIENTS_GET_LIST, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> UpdateAsync(ClientEntity client)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdClient = client.IdClient,
                    client.Names,
                   client.LastNames,
                    client.Email
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.CLIENTS_UPDATE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }






    }

}
