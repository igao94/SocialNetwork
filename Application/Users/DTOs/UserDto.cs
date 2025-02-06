using Application.Photos.DTOs;
using Application.Posts.DTOs;

namespace Application.Users.DTOs;

public class UserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int Age { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public ICollection<PhotoDto> Photos { get; set; } = [];
    public ICollection<PostDto> Posts { get; set; } = [];
}
