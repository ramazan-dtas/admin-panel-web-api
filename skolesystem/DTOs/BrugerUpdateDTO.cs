using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs
{
    public class BrugerUpdateDTO
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(90)]
        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public int GenderId { get; set; }

        public int CityId { get; set; }
    }

}
