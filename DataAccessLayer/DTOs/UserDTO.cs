namespace DataAccessLayer.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string LoginName { get; set; } = null!;
        public string Role { get; set; }
    }
}
