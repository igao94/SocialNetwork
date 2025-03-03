using Application.Core;
using Application.Photos.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Photos.GetPhotos;

public class GetPhotosForUserQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetPhotosForUserQuery, Result<List<PhotoDto>>>
{
    public async Task<Result<List<PhotoDto>>> Handle(GetPhotosForUserQuery request,
        CancellationToken cancellationToken)
    {
        var photos = await unitOfWork.PhotosRepository.GetPhotosForUserAsync(request.Username);

        return Result<List<PhotoDto>>.Success(mapper.Map<List<PhotoDto>>(photos));
    }
}
