using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skolesystem.Models
{
	public class Assignment
	{
        [Key] 
        public int assignment_id { get; set; }

        [Required]
        [MaxLength]
        [Column(TypeName = "longtext")] 
        public string assignment_description { get; set; }

        [Required] 
        public DateTime assignment_deadline { get; set; }

        public int is_Deleted { get; set; }

        public List<UserSubmission> userSubmissions { get; set; }


        [ForeignKey("Classe")]
        public int class_id { get; set; }
        public Classe Classe { get; set; }



    }
}

