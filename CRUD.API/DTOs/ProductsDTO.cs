namespace CRUD.API.DTOs
{
    public class ProductsDTO
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
    }
}
