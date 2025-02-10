using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername, string? searchTerm);
    IQueryable<AppUser> GetUserByUsernameQuery(string username);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    void DeleteUser(AppUser user);
    Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username);
    Task<AppUser?> GetUserWithPostsByUsernameAsync(string username);
    Task<AppUser?> GetUserWithPhotosAndPostsAndPostPhotosByUsernameAsync(string username);
    string? GetMainPhoto(AppUser user);
    Task<AppUser?> GetUserByIdAsync(string id);
    Task<AppUser?> GetUserByUsernameIncludingInactiveAsync(string username);
    Task<AppUser?> GetUserWithPhotosAndPostsAndPostPhotosAndFollowByUsernameAsync(string username);
}
