using System;
using skolesystem.DTOs.Enrollment.Request;
using skolesystem.DTOs.Enrollment.Response;
using skolesystem.DTOs.Skema.Request;
using skolesystem.DTOs.Skema.Response;

namespace skolesystem.Service.SkemaService
{
	public interface ISkemaService
	{
        Task<List<SkemaReadDto>> GetAllSkemaByClass(int classId);
        Task<List<SkemaReadDto>> GetAllSkema();
        Task<SkemaReadDto?> GetById(int enrollmentId);
        Task<SkemaReadDto?> Create(SkemaCreateDto newEnrollment);
        Task<SkemaReadDto?> Update(int enrollmentsId, SkemaUpdateDto updateEnrollment);
        Task<bool> Delete(int enrollmentId);
    }
}

