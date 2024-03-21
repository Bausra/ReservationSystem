
namespace ReservationSystem.WebApi.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LocationSpot> LocationSpots { get; set; }
        public Status Status { get; set; }
    }
}
