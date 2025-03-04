using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class PhotosRepository(DataContext context) : IPhotosRepository
{
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
