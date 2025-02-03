﻿using Application.Interfaces;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Photos;

public class PhotosService : IPhotosService
{
    private readonly Cloudinary _cloudinary;

    public PhotosService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<PhotoUploadResult?> AddPhotoAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width("500").Height("500").Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error is not null) throw new Exception(uploadResult.Error.Message);

            return new PhotoUploadResult(uploadResult.PublicId, uploadResult.SecureUrl.ToString());
        }

        return null;
    }

    public async Task<string> DeletePhotoAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deletionParams);

        if (result.Error is not null) throw new Exception(result.Error.Message);

        return result.Result = "Ok";
    }
}
