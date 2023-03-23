namespace CRUD.API.DTOs
{
    public class UserDTO
    {

        public int IdUser { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
    }
}
