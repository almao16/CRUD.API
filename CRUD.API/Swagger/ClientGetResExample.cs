using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class ClientGetResExample : IExamplesProvider<OperationResult<ClientDTO>>
    {
        public OperationResult<ClientDTO> GetExamples()
        {
            return new OperationResult<ClientDTO>
            {
                Err = false,
                Message = string.Empty,
                Data = new ClientDTO()
                {

                }
            };
        }
    }
}
