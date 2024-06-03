using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class Connection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConnectionId { get; set; }
        [ForeignKey("FirstUserId")]
        public User? FirstUser { get; set; }
        public int FirstUserId { get; set; }
        [ForeignKey("SecondUserId")]
        public User? SecondUser {  get; set; }
        public int SecondUserId {  get; set; }
        public DateTime CreationDate { get; set; }
    }
}
