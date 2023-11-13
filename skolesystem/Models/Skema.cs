using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace skolesystem.Models
{
    public class Skema
    {
        [Key]
        public int schedule_id { get; set; }

        public int user_subject_id { get; set; }

        public string day_of_week { get; set; }

        public int start_time { get; set; }

        public int end_time { get; set; }

        public int class_id { get; set; }

       // public string Class { get; set; }

       // public string UserSubject { get; set; }

        //[ForeignKey("ClassId")]
        //public Class Class { get; set; }

        //[ForeignKey("UserSubjectId")]
        //public UserSubject UserSubject { get; set; }
    }
}
