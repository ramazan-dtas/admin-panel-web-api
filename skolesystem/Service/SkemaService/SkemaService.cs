using System;
using skolesystem.DTOs.Classe.Response;
using skolesystem.DTOs.Enrollment.Request;
using skolesystem.DTOs.Enrollment.Response;
using skolesystem.DTOs.Skema.Request;
using skolesystem.DTOs.Skema.Response;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Repository.AssignmentRepository;
using skolesystem.Repository.ClasseRepository;
using skolesystem.Repository.EnrollmentRepository;
using skolesystem.Repository.SkemaRepository;

namespace skolesystem.Service.SkemaService
{
	public class SkemaService : ISkemaService
	{
        private readonly ISkemaRepository _SkemaRepository;
        private readonly IClasseRepository _ClasseRepository;


        public SkemaService(ISkemaRepository SkemaRepository, IClasseRepository ClasseRepository)
        {
            _SkemaRepository = SkemaRepository;
            _ClasseRepository = ClasseRepository;
        }


        public async Task<List<SkemaReadDto>> GetAllSkemaByClass(int assignmentId)
        {
            List<Skema> Enrollment = await _SkemaRepository.GetSkemaByClass(assignmentId);
            return Enrollment.Select(a => new SkemaReadDto
            {
                schedule_id = a.schedule_id,
                day_of_week = a.day_of_week,
                subject_name = a.subject_name,
                start_time = a.start_time,
                end_time = a.end_time,
                skemaClasseResponse = new SkemaClasseResponse
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                }
            }).ToList();


        }

        public async Task<List<SkemaReadDto>> GetAllSkema()
        {
            List<Skema> Classe = await _SkemaRepository.SelectAllSkema();
            return Classe.Select(a => new SkemaReadDto
            {
                schedule_id = a.schedule_id,
                day_of_week = a.day_of_week,
                subject_name = a.subject_name,
                start_time = a.start_time,
                end_time = a.end_time,
                skemaClasseResponse = new SkemaClasseResponse
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                }
            }).ToList();


        }

        public async Task<SkemaReadDto?> GetById(int EnrollmentId)
        {
            Skema a = await _SkemaRepository.SelectSkemaById(EnrollmentId);
            return a == null ? null : new SkemaReadDto
            {
                schedule_id = a.schedule_id,
                day_of_week = a.day_of_week,
                subject_name = a.subject_name,
                start_time = a.start_time,
                end_time = a.end_time,
                skemaClasseResponse = new SkemaClasseResponse
                {
                    class_id = a.Classe.class_id,
                    class_name = a.Classe.class_name,
                }
            };
        }



        public async Task<SkemaReadDto?> Create(SkemaCreateDto a)
        {
            Skema Enrollment = new Skema
            {
                schedule_id = a.schedule_id,
                day_of_week = a.day_of_week,
                subject_name = a.subject_name,
                start_time = a.start_time,
                end_time = a.end_time,
                class_id = a.class_id
            };

            Enrollment = await _SkemaRepository.InsertNewSkema(Enrollment);
            //await _UsersRepository.GetEnrollmentByAssignment(user.CategoryId);

            return Enrollment == null ? null : new SkemaReadDto
            {
                schedule_id = Enrollment.schedule_id,
                day_of_week = Enrollment.day_of_week,
                subject_name = Enrollment.subject_name,
                start_time = Enrollment.start_time,
                end_time = Enrollment.end_time,
                skemaClasseResponse = new SkemaClasseResponse
                {
                    class_id = Enrollment.Classe.class_id,
                    class_name = Enrollment.Classe.class_name,
                }
            };
        }

        public async Task<SkemaReadDto?> Update(int EnrollmentId, SkemaUpdateDto a)
        {
            Skema Enrollment = new Skema
            {
                schedule_id = a.schedule_id,
                day_of_week = a.day_of_week,
                subject_name = a.subject_name,
                start_time = a.start_time,
                end_time = a.end_time,
                class_id = a.class_id
            };

            Enrollment = await _SkemaRepository.UpdateExistingSkema(EnrollmentId, Enrollment);
            if (Enrollment == null) return null;
            else
            {
                await _SkemaRepository.SelectSkemaById(Enrollment.schedule_id);
                return Enrollment == null ? null : new SkemaReadDto
                {
                    schedule_id = Enrollment.schedule_id,
                    day_of_week = Enrollment.day_of_week,
                    subject_name = Enrollment.subject_name,
                    start_time = Enrollment.start_time,
                    end_time = Enrollment.end_time,
                    skemaClasseResponse = new SkemaClasseResponse
                    {
                        class_id = Enrollment.Classe.class_id,
                        class_name = Enrollment.Classe.class_name,
                    }
                };
            }
        }
        public async Task<bool> Delete(int EnrollmentId)
        {
            var result = await _SkemaRepository.DeleteSkema(EnrollmentId);
            return (result != null);
        }
    }
}

