using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.API.Models.DTO
{
    public class ReportPostForCreationDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [Required]
        public int ReportedPostId { get; set; }
        [Required]
        public string Report { get; set; }
    }
}
