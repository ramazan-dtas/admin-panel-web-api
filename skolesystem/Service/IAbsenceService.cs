using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;

namespace skolesystem.Service
{
    // Service for business logic
    public interface IAbsenceService
    {
        Task<Absence> GetAbsenceById(int id);
        Task<IEnumerable<Absence>> GetAllAbsences();
        Task<IEnumerable<Absence>> GetDeletedAbsences();
        Task<int> CreateAbsence(Absence absenceDto);
        Task UpdateAbsence(int id, Absence absenceDto);
        Task SoftDeleteAbsence(int id);
    }

    public class AbsenceService : IAbsenceService
    {
        private readonly IAbsenceRepository _absenceRepository;

        public AbsenceService(IAbsenceRepository absenceRepository)
        {
            _absenceRepository = absenceRepository;
        }

        public async Task<Absence> GetAbsenceById(int id)
        {
            return await _absenceRepository.GetById(id);
        }

        public async Task<IEnumerable<Absence>> GetAllAbsences()
        {
            return await _absenceRepository.GetAll();
        }

        public async Task<IEnumerable<Absence>> GetDeletedAbsences()
        {
            return await _absenceRepository.GetDeletedAbsences();
        }

        public async Task<int> CreateAbsence(Absence absenceDto)
        {
            var absence = new Absence
            {
                user_id = absenceDto.user_id,
                teacher_id = absenceDto.teacher_id,
                class_id = absenceDto.class_id,
                absence_date = absenceDto.absence_date,
                reason = absenceDto.reason
            };

            await _absenceRepository.AddAbsence(absence);

            return absence.absence_id;
        }

        public async Task UpdateAbsence(int id, Absence absenceDto)
        {
            var existingAbsence = await _absenceRepository.GetById(id);

            if (existingAbsence == null)
            {
                throw new ArgumentException("Absence not found");
            }

            // Map properties from AbsenceUpdateDto to the entity
            existingAbsence.user_id = absenceDto.user_id;
            existingAbsence.teacher_id = absenceDto.teacher_id;
            existingAbsence.class_id = absenceDto.class_id;
            existingAbsence.absence_date = absenceDto.absence_date;
            existingAbsence.reason = absenceDto.reason;

            await _absenceRepository.UpdateAbsence(id, existingAbsence);
        }

        public async Task SoftDeleteAbsence(int id)
        {
            await _absenceRepository.SoftDeleteAbsence(id);
        }
    }
}