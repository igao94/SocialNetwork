using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername, string? searchTerm);
    IQueryable<AppUser> GetUserByUsernameQuery(string username);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    void DeleteUser(AppUser user);
    Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username);
    Task<AppUser?> GetUserWithPhotosAndPostsByUsernameAsync(string username);
    string? GetMainPhoto(AppUser user);
}
