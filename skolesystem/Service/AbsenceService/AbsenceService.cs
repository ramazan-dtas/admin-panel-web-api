using System;
using skolesystem.DTOs.Absence.Request;
using skolesystem.DTOs.Absence.Response;
using skolesystem.DTOs.UserSubmission.Request;
using skolesystem.DTOs.UserSubmission.Response;
using skolesystem.Migrations.UsersDb;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Repository.AbsenceRepository;
using skolesystem.Repository.ClasseRepository;

namespace skolesystem.Service.AbsenceService
{
	public class AbsenceService : IAbsenceService
	{
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IUsersRepository _UsersRepository;
        private readonly IClasseRepository _classeRepository;

        public AbsenceService(IAbsenceRepository absenceRepository, IUsersRepository usersRepository, IClasseRepository classeRepository)
        {
            _absenceRepository = absenceRepository;
            _UsersRepository = usersRepository;
            _classeRepository = classeRepository;

        }

        public async Task<AbsenceReadDto> CreateAbsence(AbsenceCreateDto absenceDto)
        {
            Absence UserSubmission = new Absence
            {
                absence_date = absenceDto.absence_date,
                reason = absenceDto.reason,
                is_deleted = absenceDto.is_deleted,
                user_id = absenceDto.user_id,
                class_id = absenceDto.class_id,
                teacher_id = absenceDto.teacher_id
            };

            UserSubmission = await _absenceRepository.AddAbsence(UserSubmission);
            //await _UsersRepository.GetUserSubmissionByAssignment(user.CategoryId);

            return UserSubmission == null ? null : new AbsenceReadDto
            {
                absence_id = UserSubmission.absence_id,
                absence_date = UserSubmission.absence_date,
                reason = UserSubmission.reason,
                is_deleted = UserSubmission.is_deleted,
                absenceClasseRead = new AbsenceClasseRead
                {
                    class_id = UserSubmission.Classe.class_id,
                    class_name = UserSubmission.Classe.class_name,
                },
                absenceUserRead = new AbsenceUserRead
                {
                    user_id = UserSubmission.User.user_id,
                    surname = UserSubmission.User.surname,
                    email = UserSubmission.User.email
                }
            };
        }

        public async Task<AbsenceReadDto> GetAbsenceById(int id)
        {
            Absence UserSubmission = await _absenceRepository.GetById(id);
            return UserSubmission == null ? null : new AbsenceReadDto
            {
                absence_id = UserSubmission.absence_id,
                absence_date = UserSubmission. absence_date,
                reason = UserSubmission.reason,
                is_deleted = UserSubmission.is_deleted,
                absenceClasseRead = new AbsenceClasseRead
                {
                    class_id = UserSubmission.Classe.class_id,
                    class_name = UserSubmission.Classe.class_name,
                },
                absenceUserRead = new AbsenceUserRead
                {
                    user_id = UserSubmission.User.user_id,
                    surname = UserSubmission.User.surname,
                    email = UserSubmission.User.email
                }
            };
        }

        public async Task<List<AbsenceReadDto>> GetAllAbsencebyClasse(int id)
        {
            List<Absence> UserSubmission = await _absenceRepository.GetAllAbsencebyClasse(id);
            return UserSubmission.Select(a => new AbsenceReadDto
            {
                absence_id = a.absence_id,
                absence_date = a.absence_date,
                reason = a.reason,
                is_deleted = a.is_deleted,
                absenceClasseRead = new AbsenceClasseRead
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                },
                absenceUserRead = new AbsenceUserRead
                {
                    user_id = a.User.user_id,
                    surname = a.User.surname,
                    email = a.User.email
                }
            }).ToList();
        }

        public async Task<List<AbsenceReadDto>> GetAllAbsencebyUser(int id)
        {
            List<Absence> UserSubmission = await _absenceRepository.GetAllAbsencebyUser(id);
            return UserSubmission.Select(a => new AbsenceReadDto
            {
                absence_id = a.absence_id,
                absence_date = a.absence_date,
                reason = a.reason,
                is_deleted = a.is_deleted,
                absenceUserRead = new AbsenceUserRead
                {
                    user_id = a.User.user_id,
                    surname = a.User.surname,
                    email = a.User.email
                },
                absenceClasseRead = new AbsenceClasseRead
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                },
            }).ToList();
        }

        public async Task<List<AbsenceReadDto>> GetAllAbsences()
        {
            List<Absence> UserSubmission = await _absenceRepository.GetAll();
            return UserSubmission.Select(a => new AbsenceReadDto
            {
                absence_id = a.absence_id,
                absence_date = a.absence_date,
                reason = a.reason,
                is_deleted = a.is_deleted,
                absenceClasseRead = new AbsenceClasseRead
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                },
                absenceUserRead = new AbsenceUserRead
                {
                    user_id = a.User.user_id,
                    surname = a.User.surname,
                    email = a.User.email
                }
            }).ToList();
        }

        public async Task<bool> GetDeletedAbsences(int UserSubmissionId)
        {
            var result = await _absenceRepository.GetDeletedAbsences(UserSubmissionId);
            return (result != null);
        }

        public async Task SoftDeleteAbsence(int id)
        {
            await _absenceRepository.SoftDeleteAbsence(id);
        }

        public async Task<AbsenceReadDto> UpdateAbsence(int id, AbsenceUpdateDto absenceDto)
        {
            Absence UserSubmission = new Absence
            {
                absence_date = absenceDto.absence_date,
                reason = absenceDto.reason,
                user_id = absenceDto.user_id,
                class_id = absenceDto.class_id,
                teacher_id = absenceDto.teacher_id
            };

            UserSubmission = await _absenceRepository.UpdateAbsence(id, UserSubmission);
            if (UserSubmission == null) return null;
            else
            {
                await _absenceRepository.GetById(UserSubmission.absence_id);
                return UserSubmission == null ? null : new AbsenceReadDto
                {
                    absence_id = UserSubmission.absence_id,
                    absence_date = UserSubmission.absence_date,
                    reason = UserSubmission.reason,
                    is_deleted = UserSubmission.is_deleted,
                    absenceClasseRead = new AbsenceClasseRead
                    {
                        class_id = UserSubmission.Classe.class_id,
                        class_name = UserSubmission.Classe.class_name,
                    },
                    absenceUserRead = new AbsenceUserRead
                    {
                        user_id = UserSubmission.User.user_id,
                        surname = UserSubmission.User.surname,
                        email = UserSubmission.User.email
                    }
                };
            }
        }
    }
}

