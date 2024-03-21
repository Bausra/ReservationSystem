
namespace ReservationSystem.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public string? Email { get; set; }
        public string CountryAbbreviation { get; set; }
        public Status Status { get; set; }
    }
}
