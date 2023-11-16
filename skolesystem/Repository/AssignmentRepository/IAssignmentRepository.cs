using System;
using skolesystem.Models;

namespace skolesystem.Repository.AssignmentRepository
{
	public interface IAssignmentRepository
	{
        Task<List<Assignment>> SelectAllAssignment();
        Task<Assignment> SelectAssignmentById(int assignmentId);
        Task<Assignment> InsertNewAssignment(Assignment assignment);
        Task<Assignment> UpdateExistingAssignment(int assignmentId, Assignment assignment);
        Task<Assignment> DeleteAssignment(int assignmentId);
    }
}

