namespace Application.Accounts.DTOs;

public class AccountDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? Token { get; set; }
    public string? PhotoUrl { get; set; }
}
