namespace skolesystem.DTOs
{
    public class UserUpdateDto
    {
        public string surname { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public bool is_deleted { get; set; }
        public int role_id { get; set; }


    }
}
