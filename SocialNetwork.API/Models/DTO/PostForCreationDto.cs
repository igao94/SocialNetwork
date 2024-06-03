using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.API.Models.DTO
{
    public class PostForCreationDto
    {
        [JsonIgnore]
        public int UserId { get; set; } 
        [Required]
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
