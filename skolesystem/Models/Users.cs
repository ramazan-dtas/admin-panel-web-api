using System.ComponentModel.DataAnnotations;


namespace skolesystem.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public bool is_deleted { get; set; }
        public int role_id { get; set; }
        public List<UserSubmission> userSubmissions { get; set; }
    }

}
