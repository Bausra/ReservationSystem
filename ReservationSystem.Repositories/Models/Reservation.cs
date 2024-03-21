using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Repositories.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }


        [ForeignKey(nameof(LocationSpotId))]
        public LocationSpot LocationSpot { get; set; }
        public int LocationSpotId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
