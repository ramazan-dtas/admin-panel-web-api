using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skolesystem.Models
{
	public class Enrollments
	{
        [Key]
        public int enrollment_id { get; set; }

        [Required]
        public int is_deleted { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }
        public Users User { get; set; }

        [ForeignKey("Classe")]
        public int class_id { get; set; }
        public Classe Classe { get; set; }
    }
}

