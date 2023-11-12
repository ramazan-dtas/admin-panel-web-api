using System.ComponentModel.DataAnnotations;

namespace skolesystem.Models
{
    public class Skema
    {
        public int AssignmentId { get; set; }
        public int ClassId { get; set; }

        [Required]
        public string AssignmentDescription { get; set; }

        [Required]
        public DateTime AssignmentDeadline { get; set; }
        public bool IsDeleted { get; set; }

        // public Class Class { get; set; }
    }
}
