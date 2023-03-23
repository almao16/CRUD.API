using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class ClientUpdateDTO
    {
        [Required]
        public int IdClient { get; set; }

        [Required]
        public string Names { get; set; }
        [Required]
        public string LastNames { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
