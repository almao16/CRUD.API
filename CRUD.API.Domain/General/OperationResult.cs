using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.General
{
    public class OperationResult<T>
    {
        public bool Err { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default;
    }
}
