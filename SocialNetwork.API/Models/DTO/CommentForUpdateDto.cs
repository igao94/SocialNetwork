using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetwork.API.Models.DTO
{
    public class CommentForUpdateDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [Required]
        public string CommentContent { get; set; }
    }
}
