using System;
using skolesystem.DTOs.UserSubmission.Response;

namespace skolesystem.DTOs.Enrollment.Response
{
	public class EnrollmentResponse
	{
        public int enrollment_Id { get; set; }
        public EnrollmentClassResponse enrollmentClassResponse { get; set; }
        public EnrollmentUserResponse enrollmentUserResponse { get; set; }
    }
}

