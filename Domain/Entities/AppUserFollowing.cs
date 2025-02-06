namespace Domain.Entities;

public class AppUserFollowing
{
    public string ObserverId { get; set; } = null!;
    public AppUser Observer { get; set; } = null!;
    public string TargetId { get; set; } = null!;
    public AppUser Target { get; set; } = null!;
}
