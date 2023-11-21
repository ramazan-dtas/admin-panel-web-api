using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Repository.SkemaRepository
{
	public class SkemaRepository : ISkemaRepository
	{
        private readonly SkemaDbContext _context;


        public SkemaRepository(SkemaDbContext context)
        {
            _context = context;
        }

        public async Task<Skema> DeleteSkema(int SkemaId)
        {
            Skema deleteSkema = await _context.skema
                .FirstOrDefaultAsync(Skema => SkemaId == Skema.schedule_id);

            if (deleteSkema != null)
            {
                _context.skema.Remove(deleteSkema);
                await _context.SaveChangesAsync();
            }
            return deleteSkema;
        }

        public async Task<Skema> InsertNewSkema(Skema Skema)
        {
            _context.skema.Add(Skema);
            await _context.SaveChangesAsync();
            return Skema;
        }

        public async Task<Skema> UpdateExistingSkema(int SkemaId, Skema Skema)
        {
            Skema updateSkema = await _context.skema
                .FirstOrDefaultAsync(Skema => Skema.schedule_id == SkemaId);
            if (updateSkema != null)
            {
                updateSkema.day_of_week = Skema.day_of_week;
                updateSkema.subject_name = Skema.subject_name;
                updateSkema.start_time = Skema.start_time;
                updateSkema.end_time = Skema.end_time;

                await _context.SaveChangesAsync();
            }
            return updateSkema;
        }

        public async Task<List<Skema>> SelectAllSkema()
        {
            return await _context.skema.Include(p => p.Classe).ToListAsync();
        }

        public async Task<Skema> SelectSkemaById(int SkemaId)
        {
            return await _context.skema
            .Include(p => p.Classe).
                FirstOrDefaultAsync(a => a.schedule_id == SkemaId);
        }

        public async Task<List<Skema>> GetSkemaByClass(int enrollmentsId)
        {
            return await _context.skema.Where(a => a.schedule_id == enrollmentsId && a.Classe.is_deleted == false).Include(a => a.Classe).ToListAsync();
        }
    }
}

