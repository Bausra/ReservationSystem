
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Repositories.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LocationSpot> LocationSpots { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
