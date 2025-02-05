using Application.Core;
using Application.Likes.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Likes.GetPostLikes;

public class GetPostLikesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetPostLikesQuery, Result<List<UserLikeDto>>?>
{
    public async Task<Result<List<UserLikeDto>>?> Handle(GetPostLikesQuery request,
        CancellationToken cancellationToken)
    {
        var likes = await unitOfWork.LikesRepository.GetUsersWhoLikedPostAsync(request.PostId);

        return Result<List<UserLikeDto>>.Success(mapper.Map<List<UserLikeDto>>(likes));
    }
}
