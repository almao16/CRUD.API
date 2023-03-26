using System;

namespace CRUD.API.DTOs
{
    public class UserDTO
    {

        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public float Salary { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
    }
}
