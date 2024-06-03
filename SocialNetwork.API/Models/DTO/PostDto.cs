namespace SocialNetwork.API.Models.DTO
{
    public class PostDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int NumberOfComments
        {
            get
            {
                return Comments.Count();
            }
        }
        public IEnumerable<CommentDto> Comments { get; set; } =
            new List<CommentDto>();
        public int NumberOfLikes
        {
            get
            {
                return Likes.Count();
            }
        }
        public IEnumerable<LikeDto> Likes { get; set; } =
            new List<LikeDto>();
    }
}
