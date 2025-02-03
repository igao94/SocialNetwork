using Application.Core;
using MediatR;

namespace Application.Photos.DeletePhoto;

public record DeletePhotoCommand(int PhotoId) : IRequest<Result<Unit>>;
