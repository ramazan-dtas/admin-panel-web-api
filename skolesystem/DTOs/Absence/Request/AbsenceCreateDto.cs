using System;
using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs.Absence.Request
{
	public class AbsenceCreateDto
	{
        [Required]
        public int user_id { get; set; }

        [Required]
        public int teacher_id { get; set; }

        [Required]
        public int class_id { get; set; }

        [Required]
        public DateTime absence_date { get; set; }

        public string reason { get; set; }

        public bool is_deleted { get; set; } = false;

    }
}

