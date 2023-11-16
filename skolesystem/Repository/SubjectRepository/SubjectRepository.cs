using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Repository.SubjectRepository
{
	public class SubjectRepository : ISubjectRepository
	{
        private readonly SubjectsDbContext _context;


        public SubjectRepository(SubjectsDbContext context)
        {
            _context = context;
        }

        public async Task<Subjects> DeleteSubject(int SubjectId)
        {
            Subjects deleteSubject = await _context.Subjects
                .FirstOrDefaultAsync(Subject => SubjectId == Subject.subject_id);

            if (deleteSubject != null)
            {
                _context.Subjects.Remove(deleteSubject);
                await _context.SaveChangesAsync();
            }
            return deleteSubject;
        }

        public async Task<Subjects> InsertNewSubject(Subjects Subject)
        {
            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();
            return Subject;
        }

        public async Task<Subjects> UpdateExistingSubject(int SubjectId, Subjects Subject)
        {
            Subjects updateSubject = await _context.Subjects
                .FirstOrDefaultAsync(Subject => Subject.subject_id == SubjectId);
            if (updateSubject != null)
            {
                updateSubject.subject_name = Subject.subject_name;
                await _context.SaveChangesAsync();
            }
            return updateSubject;
        }

        public async Task<List<Subjects>> SelectAllSubject()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subjects> SelectSubjectById(int SubjectId)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(a => a.subject_id == SubjectId);
        }
    }
}

