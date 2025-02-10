namespace Application.Likes.DTOs;

public class LikeDto
{
    public string Username { get; set; } = string.Empty;
    public int LikedPostId { get; set; }
}
