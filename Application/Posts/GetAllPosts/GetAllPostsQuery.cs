using Application.Core;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.GetAllPosts;

public record GetAllPostsQuery : IRequest<Result<List<PostDto>>>;
