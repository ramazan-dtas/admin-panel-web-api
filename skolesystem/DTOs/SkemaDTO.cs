namespace skolesystem.DTOs
{
    public class SkemaDTO
    {
        public int AssignmentId { get; set; }
        public int ClassId { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime AssignmentDeadline { get; set; }
        public bool IsDeleted { get; set; }
    }

}
