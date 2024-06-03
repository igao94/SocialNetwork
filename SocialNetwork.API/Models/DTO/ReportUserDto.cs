namespace SocialNetwork.API.Models.DTO
{
    public class ReportUserDto
    {
        public int UserId { get; set; }
        public int ReportedUserId { get; set; }
        public string Report { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
