namespace Application.Posts.DTOs;

public class PostDto
{
    public int PostId { get; set; }
    public string Username {  get; set; } = string.Empty;
    public string? ProfilePictureUrl {  get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public ICollection<PostPhotoDto> PostPhotos { get; set; } = [];
}
