using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.API.Models.DTO
{
    public class LikeForCreationDto
    {
        [Required]
        public int PostId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
