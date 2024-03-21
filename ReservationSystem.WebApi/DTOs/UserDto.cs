using System.ComponentModel.DataAnnotations;


namespace ReservationSystem.WebApi.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters in length!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters in length!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        public string PersonalCode { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [StringLength(35, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters in length!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        public string Phone { get; set; }

        [MinLength(3, ErrorMessage = "{0} must be at least {1} characters in length!")]
        public string? CarRegistrationPlate { get; set; }

        /// <example>email@email.com</example>
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address!")]
        public string? Email { get; set; }

        /// <example>LTU</example>
        [Required(ErrorMessage = "{0} cannot be empty!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "{0} must contain only letters!")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters in length!")]
        public string CountryAbbreviation { get; set; }
    }
}
