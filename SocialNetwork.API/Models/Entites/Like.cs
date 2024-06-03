using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public int UserId {  get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
