using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.General
{
    public class GenericList<T>
    {
        public int CurrentPage { get; set; }
        public int Rows { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRows / Rows);
        public IList<T> Items { get; set; }
    }
}
