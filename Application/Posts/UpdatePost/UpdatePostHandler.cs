using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.UpdatePost;

public class UpdatePostHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<UpdatePostCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosAndPostsByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var post = unitOfWork.PostsRepository.GetPostForUserById(user, request.PostId);

        if (post is null) return null;

        mapper.Map(request, post);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update post.");
    }
}
