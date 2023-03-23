using CRUD.API.Domain.General;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class TokenResExample : IExamplesProvider<OperationResult<string>>
    {
        public OperationResult<string> GetExamples()
        {
            return new OperationResult<string>
            {
                Err = false,
                Message = string.Empty,
                Data = "session token"
            };
        }
    }
}
