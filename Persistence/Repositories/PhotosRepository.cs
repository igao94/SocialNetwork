using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Repositories;

public class PhotosRepository : IPhotosRepository
{
    public void AddPhoto(AppUser user, Photo photo) => user.Photos.Add(photo);

    public void DeletePhoto(AppUser user, Photo photo) => user.Photos.Remove(photo);

    public Photo? GetPhotoById(AppUser user, int photoId)
    {
        return user.Photos.FirstOrDefault(p => p.PhotoId == photoId);
    }

    public bool UserHasAnyPhotos(AppUser user) => user.Photos.Count != 0;

    public Photo? GetCurrentMainPhoto(AppUser user) => user.Photos.FirstOrDefault(p => p.IsMain);
}
