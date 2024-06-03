using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Comment> Comments { get; set; } =
            new List<Comment>();        
        public IEnumerable<Like> Likes { get; set; } =
            new List<Like>();
    }
}
