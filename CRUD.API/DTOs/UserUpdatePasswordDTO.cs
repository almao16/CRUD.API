using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class UserUpdatePasswordDTO
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string NewPassword { get; set; }

    }
}
