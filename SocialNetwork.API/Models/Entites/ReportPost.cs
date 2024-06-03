using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.API.Models.Entites
{
    public class ReportPost
    {
        public int ReportPostId { get; set; }
        public int UserId { get; set; }
        public int ReportedPostId { get; set; }
        public string Report { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
