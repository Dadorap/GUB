namespace DataAccessLayer.DTOs
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string LoginName { get; set; } = null!;
        public string Role { get; set; }
    }
}
