using System;
using skolesystem.Models;

namespace skolesystem.Repository.EnrollmentRepository
{
    public interface IEnrollmentRepository { 
    Task<List<Enrollments>> SelectAllEnrollments();
    Task<List<Enrollments>> GetEnrollmentsByClass(int enrollmentsId);
    Task<List<Enrollments>> GetAllEnrollmentsByUser(int enrollmentsId);
    Task<Enrollments?> SelectEnrollmentsById(int enrollmentsId);
    Task<Enrollments> InsertNewEnrollments(Enrollments enrollments);
    Task<Enrollments?> UpdateExistingEnrollments(int enrollmentsId, Enrollments enrollments);
    Task<Enrollments?> DeleteEnrollments(int enrollmentsId);
    }
}

