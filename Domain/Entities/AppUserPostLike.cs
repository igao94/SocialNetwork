namespace Domain.Entities;

public class AppUserPostLike
{
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
