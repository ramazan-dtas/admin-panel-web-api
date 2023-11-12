namespace skolesystem.DTOs
{
    public class BrugerDTO
    {
        public int UserInformationId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
    }

}
