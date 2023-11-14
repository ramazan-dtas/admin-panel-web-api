namespace skolesystem.DTOs
{
    public class AbsenceUpdateDto
    {
        public int user_id { get; set; } // The student who is absent
        public int teacher_id { get; set; } // The teacher recording the absence
        public int class_id { get; set; } // The class in which the absence occurred
        public DateTime absence_date { get; set; } // Date of the absence
        public string reason { get; set; } // Optional field for specifying the reason for the absence
    }
}
