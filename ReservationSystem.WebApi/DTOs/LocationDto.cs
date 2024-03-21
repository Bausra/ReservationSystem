using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.WebApi.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters in length!")]
        public string Name { get; set; }

        [Required, MinLength(1, ErrorMessage = "{0} must contain at least one item!")]
        public List<LocationSpotDto> LocationSpots { get; set; }
    }
}
