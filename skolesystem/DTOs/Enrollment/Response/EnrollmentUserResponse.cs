using System;
namespace skolesystem.DTOs.Enrollment.Response
{
	public class EnrollmentUserResponse
	{
        public int user_id { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public bool is_deleted { get; set; }
    }
}

