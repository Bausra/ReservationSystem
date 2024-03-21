using System.ComponentModel.DataAnnotations.Schema;


namespace ReservationSystem.Repositories.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public string? Email { get; set; }
        public List<Reservation> Reservations { get; set; }
        

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public int CountryId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
