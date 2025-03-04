using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhotosRepository
{
    Photo? GetCurrentMainPhoto(AppUser user);
    IQueryable<Photo> GetAllUserPhotosQuery(AppUser user);
    Task<List<Photo>> GetPhotosForUserAsync(string username);
}
