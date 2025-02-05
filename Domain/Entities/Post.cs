namespace Domain.Entities;

public class Post
{
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public AppUser AppUser { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public ICollection<PostPhoto> PostPhotos { get; set; } = [];
    public ICollection<AppUserPostLike> Likes { get; set; } = [];
}
