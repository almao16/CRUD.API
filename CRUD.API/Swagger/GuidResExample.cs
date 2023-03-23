using CRUD.API.Domain.General;
using Swashbuckle.AspNetCore.Filters;

namespace CRUD.API.Swagger
{
    public class GuidResExample : IExamplesProvider<OperationResult<string>>
    {
        public OperationResult<string> GetExamples()
        {
            return new OperationResult<string>
            {
                Err = false,
                Message = string.Empty,
                Data = "00000000-aaaa-0000-aaaa-000000000000"
            };
        }
    }
}
