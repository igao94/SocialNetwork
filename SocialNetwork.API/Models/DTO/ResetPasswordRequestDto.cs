using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.DTO
{
    public class ResetPasswordRequestDto
    {
        [Required, MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
