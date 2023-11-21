using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Migrations.UsersDb;
using skolesystem.Models;

namespace skolesystem.Repository.AbsenceRepository
{
	public class AbsenceRepository : IAbsenceRepository
	{
        private readonly AbsenceDbContext _context;

        public AbsenceRepository(AbsenceDbContext context)
		{
            _context = context;
		}

        public async Task<Absence> AddAbsence(Absence absence)
        {
            _context.Absence.Add(absence);
            await _context.SaveChangesAsync();
            return absence;
        }

        public async Task<List<Absence>> GetAll()
        {
            return await _context.Absence.Include(a => a.Classe).Include(u => u.User).ToListAsync();
        }

        public async Task<List<Absence>> GetAllAbsencebyClasse(int id)
        {
            return await _context.Absence.Where(a => a.absence_id == id && a.User.is_deleted == false).Include(a => a.Classe).Include(a => a.User).ToListAsync();
        }

        public async Task<List<Absence>> GetAllAbsencebyUser(int id)
        {
            return await _context.Absence.Where(a => a.absence_id == id && a.User.is_deleted == false).Include(a => a.User).Include(a => a.Classe).ToListAsync();
        }

        public async Task<Absence> GetById(int id)
        {
            return await _context.Absence
                            .Include(p => p.Classe).Include(a => a.User)
                            .FirstOrDefaultAsync(a => a.absence_id == id);
        }

        public async Task<Absence> GetDeletedAbsences(int id)
        {
            Absence deleteUserSubmission = await _context.Absence
                .FirstOrDefaultAsync(Absence => Absence.absence_id == id);

            if (deleteUserSubmission != null)
            {
                _context.Absence.Remove(deleteUserSubmission);
                await _context.SaveChangesAsync();
            }
            return deleteUserSubmission;
        }

        public async Task SoftDeleteAbsence(int id)
        {
            var userToDelete = await _context.Absence.FindAsync(id);

            if (userToDelete != null)
            {
                userToDelete.is_deleted = true;
                _context.Entry(userToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Absence> UpdateAbsence(int id, Absence absence)
        {
            Absence updateUserSubmission = await _context.Absence
                           .FirstOrDefaultAsync(UserSubmission => UserSubmission.absence_id == id);
            if (updateUserSubmission != null)
            {
                updateUserSubmission.reason = absence.reason;
                updateUserSubmission.absence_date = absence.absence_date;
                await _context.SaveChangesAsync();
            }
            return updateUserSubmission;
        }
    }
}

