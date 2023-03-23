using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class UserUpdateDTO
    {

        [Required]
        public int IdUser { get; set; }

        [Required]
        public string Names { get; set; }
        [Required]
        public string LastNames { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
