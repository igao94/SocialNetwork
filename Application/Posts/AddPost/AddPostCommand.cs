using Application.Core;
using Application.Posts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.AddPost;

public record AddPostCommand(string Content, List<IFormFile>? Files) : IRequest<Result<PostDto>>;
