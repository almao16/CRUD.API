using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public string Names { get; set; }
        [Required]
        public string LastNames { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
