﻿using System;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Repository.AssignmentRepository
{
	public class AssignmentRepository : IAssignmentRepository
	{
        private readonly AssignmentDbContext _context;


        public AssignmentRepository(AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<Assignment> DeleteAssignment(int assignmentId)
        {
            Assignment deleteAssignment = await _context.Assignments
                .FirstOrDefaultAsync(Assignment => assignmentId == Assignment.assignment_id);

            if (deleteAssignment != null)
            {
                _context.Assignments.Remove(deleteAssignment);
                await _context.SaveChangesAsync();
            }
            return deleteAssignment;
        }

        public async Task<Assignment> InsertNewAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<Assignment> UpdateExistingAssignment(int assignmentId, Assignment assignment)
        {
            Assignment updateAssignment = await _context.Assignments
                .FirstOrDefaultAsync(assignment => assignment.assignment_id == assignmentId);
            if (updateAssignment != null)
            {
                updateAssignment.class_id = assignment.class_id;
                updateAssignment.assignment_description = assignment.assignment_description;
                updateAssignment.assignment_deadline = assignment.assignment_deadline;

                await _context.SaveChangesAsync();
            }
            return updateAssignment;
        }

        public async Task<List<Assignment>> SelectAllAssignment()
        {
            return await _context.Assignments.Include(p => p.Classe).ToListAsync();
        }

        public async Task<Assignment> SelectAssignmentById(int assignmentId)
        {
            return await _context.Assignments
            .Include(p => p.Classe).
                FirstOrDefaultAsync(a => a.assignment_id == assignmentId);
        }
    }
}