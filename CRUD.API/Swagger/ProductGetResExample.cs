using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class ProductGetResExample : IExamplesProvider<OperationResult<ProductDTO>>
    {
        public OperationResult<ProductDTO> GetExamples()
        {
            return new OperationResult<ProductDTO>
            {
                Err = false,
                Message = string.Empty,
                Data = new ProductDTO()
                {

                }
            };
        }
    }
}
