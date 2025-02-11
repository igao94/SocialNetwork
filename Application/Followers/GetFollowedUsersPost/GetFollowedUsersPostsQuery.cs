using Application.Core;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Followers.GetFollowedUsersPost;

public record GetFollowedUsersPostsQuery(FeedParams FeedParams) : IRequest<Result<PagedList<PostDto>>>;
