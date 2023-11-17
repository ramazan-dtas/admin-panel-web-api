using AutoMapper;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;

namespace skolesystem.Service
{
    // Service for business logic
    public interface IAbsenceService
    {
        Task<AbsenceReadDto> GetAbsenceById(int id);
        Task<IEnumerable<Absence>> GetAllAbsences();
        Task<IEnumerable<Absence>> GetDeletedAbsences();
        Task<int> CreateAbsence(Absence absenceDto);
        Task UpdateAbsence(int id, AbsenceUpdateDto absenceDto);
        Task SoftDeleteAbsence(int id);
    }

    public class AbsenceService : IAbsenceService
    {
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IMapper _mapper;

        public AbsenceService(IAbsenceRepository absenceRepository, IMapper mapper)
        {
            _absenceRepository = absenceRepository;
            _mapper = mapper;
        }

        public async Task<AbsenceReadDto> GetAbsenceById(int id)
        {
            var absence = await _absenceRepository.GetById(id);

            if (absence == null)
            {
                return null; // or throw an exception or handle accordingly
            }

            // Use your mapping logic here to convert Absence to AbsenceReadDto
            var absenceDto = _mapper.Map<AbsenceReadDto>(absence);

            return absenceDto;
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

        public async Task UpdateAbsence(int id, AbsenceUpdateDto absenceDto)
        {
            var existingAbsence = await _absenceRepository.GetById(id);

            if (existingAbsence == null)
            {
                throw new ArgumentException("Absence not found");
            }
            existingAbsence.user_id = absenceDto.user_id;
            existingAbsence.teacher_id = absenceDto.teacher_id;
            existingAbsence.class_id = absenceDto.class_id;
            existingAbsence.absence_date = absenceDto.absence_date;
            existingAbsence.reason = absenceDto.reason;

            await _absenceRepository.UpdateAbsence(id, existingAbsence);
        }

        public async Task SoftDeleteAbsence(int id)
        {
            var absenceToDelete = await _absenceRepository.GetById(id);

            if (absenceToDelete == null)
            {
                throw new ArgumentException("Absence not found");
            }

            await _absenceRepository.SoftDeleteAbsence(id);
        }

    }
}
