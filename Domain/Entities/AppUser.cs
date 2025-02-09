using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Photo> Photos { get; set; } = [];
    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<AppUserPostLike> LikedPosts { get; set; } = [];
    public ICollection<AppUserPostComment> Comments { get; set; } = [];
    public ICollection<AppUserFollowing> Followers { get; set; } = [];
    public ICollection<AppUserFollowing> Following { get; set; } = [];
    public ICollection<UserReport> ReportsMade { get; set; } = [];
    public ICollection<UserReport> ReportsReceived { get; set; } = [];
    public ICollection<PostReport> ReportedPosts { get; set; } = [];
}
