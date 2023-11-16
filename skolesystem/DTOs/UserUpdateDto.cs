namespace skolesystem.DTOs
{
    public class UserUpdateDto
    {
        public int user_id { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public int? email_confirmed { get; set; }
        public int? lockout_enabled { get; set; }
        public int? phone_confirmed { get; set; }
        public int? twofactor_enabled { get; set; }
        public int? try_failed_count { get; set; }
        public int? lockout_end { get; set; }
        public int user_information_id { get; set; }
        public bool is_deleted { get; set; }
    }
}
