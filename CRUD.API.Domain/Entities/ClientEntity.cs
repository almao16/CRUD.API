using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
    public class ClientEntity
    {
        public int IdClient { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string CompanyEmail { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
