namespace Domain.Entities;

public class Photo
{
    public int PhotoId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public string PublicId { get; set; } = string.Empty;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}
