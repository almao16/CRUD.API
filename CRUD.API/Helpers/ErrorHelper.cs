using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace CRUD.API.Helpers
{
    public static class ErrorHelper
    {
        public static string ErrorsToString(ModelStateDictionary modelState)
        {
            return string.Join(string.Empty, modelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList());
        }
    }
}
