using Application.Core;
using Application.Photos.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Photos.AddPhoto;

public record AddPhotoCommand(IFormFile File) : IRequest<Result<PhotoDto>>;
