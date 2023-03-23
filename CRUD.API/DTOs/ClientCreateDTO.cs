using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class ClientCreateDTO
    {
        [Required]
        public string DniClient { get; set; }
        [Required]
        public string Names { get; set; }
        [Required]
        public string LastNames { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
