
using CRUD.API.Domain.General;
using CRUD.API.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace CRUD.API.Swagger
{
    public class ClientGetListResExample : IExamplesProvider<OperationResult<GenericList<ClientsDTO>>>
    {
        public OperationResult<GenericList<ClientsDTO>> GetExamples()
        {
            return new OperationResult<GenericList<ClientsDTO>>
            {
                Err = false,
                Message = string.Empty,
                Data = new GenericList<ClientsDTO>()
                {
                    CurrentPage = 1,
                    Rows = 10,
                    TotalRows = 100,
                    Items = new List<ClientsDTO>()
                    {

                    }
                }
            };
        }
    }
}
