using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models.Entites
{
    public class ResetPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResetPasswordId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
