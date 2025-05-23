﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAccountsRepository
{
    Task<bool> IsUsernameTakenAsync(string username);
    Task<bool> IsEmailTakenAsync(string email);
    Task<IdentityResult> CreateUserAsync(AppUser user, string password);
    Task<IdentityResult> AddUserToRoleAsync(AppUser user, string role);
    Task<AppUser?> GetUserWithPhotosByEmailIncludingInactiveAsync(string email);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<string> GenerateResetPasswordTokenAsync(AppUser user);
    Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string password);
    Task<bool> IsUserInRoleAsync(AppUser user, string role);
}
