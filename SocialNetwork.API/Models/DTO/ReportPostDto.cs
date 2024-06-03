namespace SocialNetwork.API.Models.DTO
{
    public class ReportPostDto
    {
        public int UserId { get; set; }
        public int ReportedPostId { get; set; }
        public string Report { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
