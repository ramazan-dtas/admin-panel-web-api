using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skolesystem.Models
{
	public class Classe
	{
        [Key]
        public int class_id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string class_name { get; set; }

        [Required]
        public bool is_deleted { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string location { get; set; }

        public List<Assignment> assignments { get; set; }
        public List<Enrollments> enrollments { get; set; }

    }
}

