using System.ComponentModel.DataAnnotations;

namespace TalabatPro.Api.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string DispalayName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@gmail\.com$",
        ErrorMessage = "Email must be a valid Gmail address (example: user@gmail.com).")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$",
        ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        //ErrorMessage = "Password must be at least 8 characters long, contain one uppercase, one lowercase, one number, and one special character.")]
        public string Password { get; set; }
    }
}
