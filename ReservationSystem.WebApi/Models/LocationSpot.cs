using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.WebApi.Models
{
    public class LocationSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }


        [ForeignKey(nameof(LocationId))]
        public Location Location{ get; set; }
        public int LocationId { get; set; }    
    }
}
