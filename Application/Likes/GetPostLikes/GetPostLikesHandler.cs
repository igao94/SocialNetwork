using Application.Core;
using Application.Likes.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Likes.GetPostLikes;

public class GetPostLikesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetPostLikesQuery, Result<PagedList<UserLikeDto>>?>
{
    public async Task<Result<PagedList<UserLikeDto>>?> Handle(GetPostLikesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.LikesParams.PostId <= 0)
            return Result<PagedList<UserLikeDto>>.Failure("Post id must be greater than 0.");

        var likesQuery = unitOfWork.LikesRepository.GetUsersWhoLikedPostQuery(request.LikesParams.PostId);

        var likes = await PagedList<UserLikeDto>
            .CreateAsync(likesQuery.ProjectTo<UserLikeDto>(mapper.ConfigurationProvider),
            request.LikesParams.PageNumber,
            request.LikesParams.PageSize);

        return Result<PagedList<UserLikeDto>>.Success(likes);
    }
}
