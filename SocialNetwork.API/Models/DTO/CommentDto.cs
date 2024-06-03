namespace SocialNetwork.API.Models.DTO
{
    public class CommentDto
    {
        public int CommentId {  get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreationDate {  get; set; }
    }
}
