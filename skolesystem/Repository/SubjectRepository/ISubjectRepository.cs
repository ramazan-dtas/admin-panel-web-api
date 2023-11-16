using System;
using skolesystem.Models;

namespace skolesystem.Repository.SubjectRepository
{
	public interface ISubjectRepository
	{
        Task<List<Subjects>> SelectAllSubject();
        Task<Subjects> SelectSubjectById(int subjectId);
        Task<Subjects> InsertNewSubject(Subjects subject);
        Task<Subjects> UpdateExistingSubject(int subjectId, Subjects subject);
        Task<Subjects> DeleteSubject(int subjectId);
    }
}

