using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhotosRepository
{
    void AddPhoto(AppUser user, Photo photo);
    void DeletePhoto(AppUser user, Photo photo);
    Photo? GetPhotoById(AppUser user, int photoId);
    bool UserHasAnyPhotos(AppUser user);
    Photo? GetCurrentMainPhoto(AppUser user);
    IQueryable<Photo> GetAllUserPhotosQuery(AppUser user);
}
