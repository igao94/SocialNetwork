using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IPhotosService
{
    Task<PhotoUploadResult?> AddPhotoAsync(IFormFile file);
    Task<string> DeletePhotoAsync(string publicId);
}
