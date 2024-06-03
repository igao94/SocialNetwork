using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.API.Models.Entites
{
    public class ReportUser
    {
        public int ReportUserId { get; set; }
        public int UserId { get; set; }
        public int ReportedUserId { get; set; }
        public string Report { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
