using Application.Core;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.GetAllPosts;

public record GetAllPostsQuery(PostsParams PostsParams) : IRequest<Result<PagedList<PostDto>>>;
