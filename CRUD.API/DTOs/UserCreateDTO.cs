using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public float Salary { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
