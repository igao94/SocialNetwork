using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.API.Models.DTO
{
    public class ReportUserForCreationDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [Required]
        public int ReportedUserId { get; set; }
        [Required]
        public string Report { get; set; }
    }
}
