using Application.Core;
using Application.Posts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.AddPost;

public record AddPostCommand(List<IFormFile>? Files, string Content) : IRequest<Result<PostDto>>;
