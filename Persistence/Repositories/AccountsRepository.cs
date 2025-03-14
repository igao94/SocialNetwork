﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AccountsRepository(UserManager<AppUser> userManager) : IAccountsRepository
{
    public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<AppUser?> GetUserWithPhotosByEmailIncludingInactiveAsync(string email)
    {
        return await userManager.Users
            .IgnoreQueryFilters()
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<string> GenerateResetPasswordTokenAsync(AppUser user)
    {
        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string password)
    {
        return await userManager.ResetPasswordAsync(user, token, password);
    }

    public async Task<bool> IsUserInRoleAsync(AppUser user, string role)
    {
        return await userManager.IsInRoleAsync(user, role);
    }
}
