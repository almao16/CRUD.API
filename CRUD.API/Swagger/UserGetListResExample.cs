using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace CRUD.API.Swagger
{
    public class UserGetListResExample : IExamplesProvider<OperationResult<GenericList<UsersDTO>>>
    {
        public OperationResult<GenericList<UsersDTO>> GetExamples()
        {
            return new OperationResult<GenericList<UsersDTO>>
            {
                Err = false,
                Message = string.Empty,
                Data = new GenericList<UsersDTO>()
                {
                    CurrentPage = 1,
                    Rows = 10,
                    TotalRows = 100,
                    Items = new List<UsersDTO>()
                    {

                    }
                }
            };
        }
    }
}
