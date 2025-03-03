using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class PhotosRepository(DataContext context) : IPhotosRepository
{
    public void AddPhoto(AppUser user, Photo photo) => user.Photos.Add(photo);

    public void DeletePhoto(AppUser user, Photo photo) => user.Photos.Remove(photo);

    public Photo? GetPhotoById(AppUser user, int photoId)
    {
        return user.Photos.FirstOrDefault(p => p.PhotoId == photoId);
    }

    public bool UserHasAnyPhotos(AppUser user) => user.Photos.Count != 0;

    public Photo? GetCurrentMainPhoto(AppUser user) => user.Photos.FirstOrDefault(p => p.IsMain);

    public IQueryable<Photo> GetAllUserPhotosQuery(AppUser user) => user.Photos.AsQueryable();

    public async Task<List<Photo>> GetPhotosForUserAsync(string username)
    {
        return await context.Users
            .Where(u => u.UserName == username)
            .SelectMany(u => u.Photos)
            .ToListAsync();
    }
}
