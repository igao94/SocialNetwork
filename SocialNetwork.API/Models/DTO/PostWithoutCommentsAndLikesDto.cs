namespace SocialNetwork.API.Models.DTO
{
    public class PostWithoutCommentsAndLikesDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
