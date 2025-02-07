namespace Domain.Entities;

public class UserReport
{
    public string ReporterId { get; set; } = null!;
    public AppUser Reporter { get; set; } = null!;
    public string ReportedUserId { get; set; } = null!;
    public AppUser ReportredUser { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
