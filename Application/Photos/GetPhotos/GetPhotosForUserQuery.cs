using Application.Core;
using Application.Photos.DTOs;
using MediatR;

namespace Application.Photos.GetPhotos;

public record GetPhotosForUserQuery(string Username) : IRequest<Result<List<PhotoDto>>>;