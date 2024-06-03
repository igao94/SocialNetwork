using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        public int PostId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
