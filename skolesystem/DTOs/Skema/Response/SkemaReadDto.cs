using System;
using skolesystem.DTOs.Assignment.Response;

namespace skolesystem.DTOs.Skema.Response
{
	public class SkemaReadDto
	{
        public int schedule_id { get; set; }

        public string day_of_week { get; set; }

        public string subject_name { get; set; }

        public string start_time { get; set; }

        public string end_time { get; set; }

        public SkemaClasseResponse skemaClasseResponse { get; set; }
    }
}

