﻿namespace Application.Accounts.DTOs;

public class ResetPasswordDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Token {  get; set; }
}
