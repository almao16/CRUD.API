using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
    public class ClientEntity
    {
        public int IdClient { get; set; }
        public string DniClient { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

    }
}
