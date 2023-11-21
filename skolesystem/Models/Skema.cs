using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace skolesystem.Models
{
    public class Skema
    {
        [Key]
        public int schedule_id { get; set; }

        public string day_of_week { get; set; }

        public string subject_name { get; set; }

        public string start_time { get; set; }

        public string end_time { get; set; }

        [ForeignKey("Classe")]
        public int class_id { get; set; }
        public Classe Classe { get; set; }
    }
}
