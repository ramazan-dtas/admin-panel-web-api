using System;
namespace skolesystem.DTOs.Assignment.Response
{
	public class AssignmentResponse
	{
        public int assignment_id { get; set; }
        public string assignment_description { get; set; }

        public DateTime assignment_deadline { get; set; }

        public AssignmentClasseResponse Classe { get; set; }
    }
}

