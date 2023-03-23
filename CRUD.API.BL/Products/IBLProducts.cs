using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Products
{
   
    public interface IBLProducts
    {

        public Task<string> CreateAsync(ProductEntity product);
        public Task<bool> DeleteAsync(int IdProduct);
        public Task<ProductEntity> GetAsync(int IdProduct);
        public Task<IList<ProductsEntity>> GetListAsync(int Page, int Rows, string Name);
        public Task<bool> UpdateAsync(ProductEntity product);

    }
}
