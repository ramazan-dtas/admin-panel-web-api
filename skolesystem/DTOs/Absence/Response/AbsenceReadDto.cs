﻿using System;
using skolesystem.DTOs.UserSubmission.Response;

namespace skolesystem.DTOs.Absence.Response
{
	public class AbsenceReadDto
	{
        public int absence_id { get; set; }
        public int user_id { get; set; }
        public int teacher_id { get; set; }
        public int class_id { get; set; }
        public DateTime absence_date { get; set; }
        public string reason { get; set; }
        public bool is_deleted { get; set; }

        public AbsenceClasseRead absenceClasseRead { get; set; }
        public AbsenceUserRead absenceUserRead { get; set; }
    }
}