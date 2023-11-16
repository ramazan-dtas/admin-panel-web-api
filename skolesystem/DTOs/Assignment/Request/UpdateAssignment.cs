using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace skolesystem.DTOs.Assignment.Request
{
	public class UpdateAssignment
	{
        [Required]
        [Range(1, int.MaxValue)]
        public int classeId { get; set; }

        [Required]
        public DateTime assignment_Deadline { get; set; }

        [Required]
        public string assignment_Description { get; set; }
    }
}

