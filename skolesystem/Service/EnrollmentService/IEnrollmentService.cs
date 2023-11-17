using System;
using skolesystem.DTOs.Enrollment.Request;
using skolesystem.DTOs.Enrollment.Response;
using skolesystem.Models;

namespace skolesystem.Service.EnrollmentService
{
	public interface IEnrollmentService
	{
        Task<List<EnrollmentResponse>> GetAllEnrollmentsByClass(int classId);
        Task<List<EnrollmentResponse>> GetAllEnrollmentsByUser(int userId);
        Task<EnrollmentResponse?> GetById(int enrollmentId);
        Task<EnrollmentResponse?> Create(NewEnrollment newEnrollment);
        Task<EnrollmentResponse?> Update(int enrollmentsId, UpdateEnrollment updateEnrollment);
        Task<bool> Delete(int enrollmentId);
    }
}

