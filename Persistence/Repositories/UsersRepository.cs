using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class UsersRepository(DataContext context) : IUsersRepository
{
    public IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername, string? searchTerm)
    {
        IQueryable<AppUser> query = context.Users.Where(u => u.UserName != currentUserUsername);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.FirstName.Contains(searchTerm)
            || u.LastName.Contains(searchTerm)
            || u.UserName!.Contains(searchTerm)
            || u.Email!.Contains(searchTerm));
        }

        return query;
    }

    public IQueryable<AppUser> GetUserByUsernameQuery(string username)
    {
        IQueryable<AppUser> query = context.Users.Where(u => u.UserName == username.ToLower());

        return query;
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }    
    
    public async Task<AppUser?> GetUserByUsernameIncludingInactiveAsync(string username)
    {
        return await context.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }

    public async Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }

    public async Task<AppUser?> GetUserWithPostsByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }

    public async Task<AppUser?> GetUserWithPhotosAndPostsAndPostPhotosByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Photos)
            .Include(u => u.Posts)
                .ThenInclude(u => u.PostPhotos)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }  
    
    public async Task<AppUser?> GetUserWithPhotosAndPostsAndPostPhotosAndFollowByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Photos)
            .Include(u => u.Posts)
                .ThenInclude(u => u.PostPhotos)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower());
    }

    public void DeleteUser(AppUser user) => context.Users.Remove(user);

    public string? GetMainPhoto(AppUser user) => user.Photos.FirstOrDefault(p => p.IsMain)?.Url;

    public async Task<AppUser?> GetUserByIdAsync(string id) => await context.Users.FindAsync(id);
}
