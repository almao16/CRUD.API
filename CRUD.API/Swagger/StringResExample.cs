using CRUD.API.Domain.General;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class StringResExample : IExamplesProvider<OperationResult<string>>
    {
        public OperationResult<string> GetExamples()
        {
            return new OperationResult<string>
            {
                Err = false,
                Message = string.Empty,
                Data = "example"
            };
        }
    }
}
