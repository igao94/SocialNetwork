using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public ICollection<Photo> Photos { get; set; } = [];
    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<AppUserPostLike> LikedPosts { get; set; } = [];
    public ICollection<AppUserPostComment> Comments { get; set; } = [];
}
