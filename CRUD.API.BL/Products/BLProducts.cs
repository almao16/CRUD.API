using Autofac;
using CRUD.API.BL.Utils;
using CRUD.API.DAL.Common;
using CRUD.API.DAL.Container;
using CRUD.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.API.BL.Products
{
    

    public class BLProducts : IBLProducts
    {
        public async Task<string> CreateAsync(ProductEntity product)
        {
            string retValue = string.Empty;
            try
            {
                object parammeters = new
                {
                    product.Name,
                   product.Description,
                    product.Stock
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteScalarAsync(ConstantsProcedures.PRODUCTS_CREATE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> DeleteAsync(int IdProduct)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdProduct
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.PRODUCTS_DELETE, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<ProductEntity> GetAsync(int IdProduct)
        {
            ProductEntity retValue = null;
            try
            {
                object parammeters = new
                {
                    IdProduct
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().GetAsync<ProductEntity>(ConstantsProcedures.PRODUCTS_GET, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<IList<ProductsEntity>> GetListAsync(int Page, int Rows, string Name)
        {
            List<ProductsEntity> retValue = null;
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
                    retValue = (List<ProductsEntity>)await scope.Resolve<IDALCommon>().GetListAsync<ProductsEntity>(ConstantsProcedures.PRODUCTS_GET_LIST, parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> UpdateAsync(ProductEntity product)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    IdProduct = product.IdProduct,
                    product.Name,
                    product.Description,
                    product.Stock
                };

                using (var scope = DALContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<IDALCommon>().ExecuteAsync(ConstantsProcedures.PRODUCTS_UPDATE, parammeters);
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
