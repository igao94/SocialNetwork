using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<Connection> Connections { get; set; } =
            new List<Connection>();
        public IEnumerable<Post> Posts { get; set; } =
            new List<Post>();
    }
}
