using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs
{
    public class BrugerReadDto
    {
        public int user_information_id { get; set; }

        [MaxLength(40)]
        public string name { get; set; }

        [MaxLength(60)]
        public string last_name { get; set; }

        [MaxLength(20)]
        public string phone { get; set; }

        [MaxLength(25)]
        public string date_of_birth { get; set; }

        [MaxLength(90)]
        public string address { get; set; }

        public bool is_deleted { get; set; }

        public int gender_id { get; set; }

        public int city_id { get; set; }
    }
}
