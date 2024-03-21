
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Repositories.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Shorthand { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int StatusId { get; set; }
    }
}
