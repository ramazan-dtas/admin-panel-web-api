using System;
using skolesystem.DTOs.Subject.Request;
using skolesystem.DTOs.Subject.Response;

namespace skolesystem.Service.SubjectService
{
	public interface ISubjectService
	{
        Task<List<SubjectResponse>> GetAll();
        Task<SubjectResponse> GetById(int subjectId);
        Task<SubjectResponse> Create(NewSubject newSubject);
        Task<SubjectResponse> Update(int subjectId, UpdateSubject updateSubject);
        Task<bool> Delete(int subjectId);
    }
}

