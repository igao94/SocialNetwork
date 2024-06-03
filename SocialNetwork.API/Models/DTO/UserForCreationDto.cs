using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.DTO
{
    public class UserForCreationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
