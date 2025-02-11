using Application.Photos.DTOs;

namespace Application.Accounts.DTOs;

public class CurrentUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? MainPhotoUrl { get; set; }
    public ICollection<PhotoDto> Photos { get; set; } = [];
}
