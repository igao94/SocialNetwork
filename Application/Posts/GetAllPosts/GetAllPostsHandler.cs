using Application.Core;
using Application.Posts.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.GetAllPosts;

public class GetAllPostsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllPostsQuery, Result<PagedList<PostDto>>?>
{
    public async Task<Result<PagedList<PostDto>>?> Handle(GetAllPostsQuery request,
        CancellationToken cancellationToken)
    {
        var postsQuery = unitOfWork.PostsRepository.GetAllPostsQuery();

        var posts = await PagedList<PostDto>
            .CreateAsync(postsQuery.ProjectTo<PostDto>(mapper.ConfigurationProvider),
            request.PostsParams.PageNumber,
            request.PostsParams.PageSize);

        return Result<PagedList<PostDto>>.Success(posts);
    }
}
