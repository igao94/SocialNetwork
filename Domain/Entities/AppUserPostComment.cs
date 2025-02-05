namespace Domain.Entities;

public class AppUserPostComment
{
    public int Id { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
