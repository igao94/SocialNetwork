using Application.Comments.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Comments.AddComment;

public class AddCommentHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<AddCommentCommand, Result<CommentDto>?>
{
    public async Task<Result<CommentDto>?> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var post = await unitOfWork.PostsRepository.GetPostByIdAsync(request.PostId);

        if (post is null) return null;

        var comment = new AppUserPostComment
        {
            AppUser = user,
            AppUserId = user.Id,
            Post = post,
            PostId = post.PostId,
            Content = request.Content
        };

        unitOfWork.CommentsRepository.AddComment(comment);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<CommentDto>.Success(mapper.Map<CommentDto>(comment))
            : Result<CommentDto>.Failure("Failed to add comment.");
    }
}
