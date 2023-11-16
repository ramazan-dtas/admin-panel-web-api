namespace skolesystem.DTOs
{
    public class LoginResponse
    {
        public int user_id { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public bool is_deleted { get; set; }
        public int role_id { get; set; }
        public string Token { get; set; }
    }

}
