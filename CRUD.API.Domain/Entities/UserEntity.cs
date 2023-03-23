using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
    public class UserEntity
    {
        public int IdUser { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

    }
}
