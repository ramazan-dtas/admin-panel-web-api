using skolesystem.Migrations.UsersDb;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace skolesystem.Models
{
    public class Absence
    {
        [Key]
        public int absence_id { get; set; }

        [Required]
        public int user_id { get; set; } // The student who is absent

        [Required]
        public int teacher_id { get; set; } // The teacher recording the absence

        [Required]
        public int class_id { get; set; } // The class in which the absence occurred

        [Required]
        public DateTime absence_date { get; set; } // Date of the absence

        public string reason { get; set; } // Optional field for specifying the reason for the absence

        public bool is_deleted { get; set; } = false;

        // Navigation properties
        //[ForeignKey(nameof(user_id))]
        //public Users User { get; set; }

        //[ForeignKey(nameof(teacher_id))]
        //public Users Teacher { get; set; }

        //[ForeignKey(nameof(class_id))]
        //public Class Class { get; set; }
    }

}
