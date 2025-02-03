using Application.Core;
using MediatR;

namespace Application.Photos.SetMainPhoto;

public record SetMainPhotoCommand(int PhotoId) : IRequest<Result<Unit>>;
