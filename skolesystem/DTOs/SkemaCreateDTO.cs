using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs
{
    public class SkemaCreateDTO
    {
        [Required]
        public string AssignmentDescription { get; set; }
        [Required]
        public DateTime AssignmentDeadline { get; set; }
        public bool IsDeleted { get; set; }
    }

}
