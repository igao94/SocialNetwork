namespace SocialNetwork.API.Models.DTO
{
    public class ConnectionDto
    {
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
