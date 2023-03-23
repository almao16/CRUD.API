using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class UserGetResExample : IExamplesProvider<OperationResult<UserDTO>>
    {
        public OperationResult<UserDTO> GetExamples()
        {
            return new OperationResult<UserDTO>
            {
                Err = false,
                Message = string.Empty,
                Data = new UserDTO()
                {

                }
            };
        }
    }
}
