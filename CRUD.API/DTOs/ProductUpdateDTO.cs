using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{
    public class ProductUpdateDTO
    {
        [Required]
        public int IdProduc { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
