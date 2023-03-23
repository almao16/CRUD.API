using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Clients
{
    public interface IBLClients
    {

        public Task<string> CreateAsync(ClientEntity client);
        public Task<bool> DeleteAsync(int IdClient);
        public Task<ClientEntity> GetAsync(int IdClient);
        public Task<IList<ClientsEntity>> GetListAsync(int Page, int Rows, string Name);
        public Task<bool> UpdateAsync(ClientEntity client);

    }
}
