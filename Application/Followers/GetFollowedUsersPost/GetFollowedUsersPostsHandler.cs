using Application.Core;
using Application.Interfaces;
using Application.Posts.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Followers.GetFollowedUsersPost;

public class GetFollowedUsersPostsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetFollowedUsersPostsQuery, Result<PagedList<PostDto>>>
{
    public async Task<Result<PagedList<PostDto>>> Handle(GetFollowedUsersPostsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var postsQuery = unitOfWork.FollowingsRepository.GetPostsFromFollowedUsersQuery(userId);

        var posts = await PagedList<PostDto>
            .CreateAsync(postsQuery.ProjectTo<PostDto>(mapper.ConfigurationProvider),
            request.FeedParams.PageNumber,
            request.FeedParams.PageSize);

        return Result<PagedList<PostDto>>.Success(posts);
    }
}
