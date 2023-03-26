using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class ClientUpdateDTO
    {
        [Required]
        public int IdClient { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string CompanyEmail { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
