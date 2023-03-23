using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace CRUD.API.Swagger
{
    public class ProductGetListResExample : IExamplesProvider<OperationResult<GenericList<ProductsDTO>>>
    {
        public OperationResult<GenericList<ProductsDTO>> GetExamples()
        {
            return new OperationResult<GenericList<ProductsDTO>>
            {
                Err = false,
                Message = string.Empty,
                Data = new GenericList<ProductsDTO>()
                {
                    CurrentPage = 1,
                    Rows = 10,
                    TotalRows = 100,
                    Items = new List<ProductsDTO>()
                    {

                    }
                }
            };
        }
    }
}
