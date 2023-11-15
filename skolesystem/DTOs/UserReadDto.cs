namespace skolesystem.DTOs
{
    public class UserReadDto
    {
        public int user_id { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public bool is_deleted { get; set; }
    }
}
