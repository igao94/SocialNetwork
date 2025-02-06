using Application.Core;
using Application.Interfaces;
using Application.Posts.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers.GetFollowedUsersPost;

public class GetFollowedUsersPostsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetFollowedUsersPostsQuery, Result<List<PostDto>>>
{
    public async Task<Result<List<PostDto>>> Handle(GetFollowedUsersPostsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var postsQuery = unitOfWork.FollowingsRepository.GetPostsFromFollowedUsersQuery(userId);

        var posts = await postsQuery.ProjectTo<PostDto>(mapper.ConfigurationProvider).ToListAsync();

        return Result<List<PostDto>>.Success(posts);
    }
}
