
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Repositories.Models
{
    public class LocationSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location Location{ get; set; }
        public int LocationId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
