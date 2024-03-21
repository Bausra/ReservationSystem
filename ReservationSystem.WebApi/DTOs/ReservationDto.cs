using ReservationSystem.WebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ReservationSystem.WebApi.Helpers.DataAnnotations;

namespace ReservationSystem.WebApi.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }

        [Required]
        [DateTimeNotInThePast]
        [StartDateBeforeEndDate]
        [CustomMinutesAndSeconds]
        public DateTime ReservationStart { get; set; }

        [Required]
        [CustomMinutesAndSeconds]
        public DateTime ReservationEnd { get; set; }

        [Required]
        [ValidEntityId(typeof(LocationSpot))]
        public int LocationSpotId { get; set; }

        [Required]
        [ValidEntityId(typeof(User))]
        public int UserId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}
