using Domain.Entities;
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

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
    }
}
