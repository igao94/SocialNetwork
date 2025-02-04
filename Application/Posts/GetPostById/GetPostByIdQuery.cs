using Application.Core;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.GetPostById;

public record GetPostByIdQuery(int PostId) : IRequest<Result<PostDto>>;
