using System;
using skolesystem.DTOs.Absence.Request;
using skolesystem.DTOs.Absence.Response;
using skolesystem.Models;

namespace skolesystem.Service.AbsenceService
{
	public interface IAbsenceService
	{
        Task<List<AbsenceReadDto>> GetAllAbsencebyClasse(int id);
        Task<List<AbsenceReadDto>> GetAllAbsencebyUser(int id);
        Task<List<AbsenceReadDto>> GetAllAbsences();
        Task<AbsenceReadDto> GetAbsenceById(int id);
        Task<bool> GetDeletedAbsences(int UserSubmissionId);
        Task<AbsenceReadDto> CreateAbsence(AbsenceCreateDto absenceDto);
        Task<AbsenceReadDto> UpdateAbsence(int id, AbsenceUpdateDto absenceDto);
        Task SoftDeleteAbsence(int id);
        
    }
}

