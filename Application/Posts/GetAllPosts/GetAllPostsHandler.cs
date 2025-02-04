using Application.Core;
using Application.Posts.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.GetAllPosts;

public class GetAllPostsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllPostsQuery, Result<List<PostDto>>?>
{
    public async Task<Result<List<PostDto>>?> Handle(GetAllPostsQuery request,
        CancellationToken cancellationToken)
    { 
        var postsQuery = unitOfWork.PostsRepository.GetAllPostsQuery();

        var posts = await postsQuery
            .ProjectTo<PostDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<PostDto>>.Success(posts);
    }
}
