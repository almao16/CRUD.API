using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class ProductCreateDTO
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
