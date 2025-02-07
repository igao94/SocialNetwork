namespace Domain.Entities;

public class PostReport
{
    public string ReporterId { get; set; } = null!;
    public AppUser Reporter { get; set; } = null!;
    public int ReportedPostId { get; set; }
    public Post ReportedPost { get; set; } = null!;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
    