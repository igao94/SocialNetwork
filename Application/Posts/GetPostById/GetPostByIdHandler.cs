using Application.Core;
using Application.Posts.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.GetPostById;

public class GetPostByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetPostByIdQuery, Result<PostDto>?>
{
    public async Task<Result<PostDto>?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var postQuery = unitOfWork.PostsRepository.GetPostByIdQuery(request.PostId);

        var post = await postQuery
            .ProjectTo<PostDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return post is null ? null : Result<PostDto>.Success(post);
    }
}
