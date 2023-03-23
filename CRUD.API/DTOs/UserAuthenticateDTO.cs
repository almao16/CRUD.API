using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class UserAuthenticateDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
