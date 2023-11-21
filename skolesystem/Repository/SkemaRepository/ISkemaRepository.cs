using System;
using skolesystem.Models;

namespace skolesystem.Repository.SkemaRepository
{
	public interface ISkemaRepository
	{
        Task<List<Skema>> SelectAllSkema();
        Task<Skema> SelectSkemaById(int SkemaId);
        Task<Skema> InsertNewSkema(Skema Skema);
        Task<Skema> UpdateExistingSkema(int SkemaId, Skema Skema);
        Task<Skema> DeleteSkema(int SkemaId);
        Task<List<Skema>> GetSkemaByClass(int enrollmentsId);
    }
}

