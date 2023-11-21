using System;
using skolesystem.Models;

namespace skolesystem.Repository.AbsenceRepository
{
	public interface IAbsenceRepository
	{
        Task<List<Absence>> GetAll();
        Task<List<Absence>> GetAllAbsencebyClasse(int id);
        Task<List<Absence>> GetAllAbsencebyUser(int id);
        Task<Absence> GetById(int id);
        Task<Absence> GetDeletedAbsences(int id);
        Task<Absence> AddAbsence(Absence absence);
        Task<Absence> UpdateAbsence(int id, Absence absence);
        Task SoftDeleteAbsence(int id);
        
    }
}

