using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CommentsRepository(DataContext context) : ICommentsRepository
{
    public void AddComment(AppUserPostComment comment) => context.AppUserPostComment.Add(comment);

    public void RemoveComment(AppUserPostComment comment) => context.AppUserPostComment.Remove(comment);

    public async Task DeleteCommentsByPostIdsAsync(List<int> postIds)
    {
        var comments = await context.AppUserPostComment
            .Where(c => postIds.Contains(c.PostId))
            .ToListAsync();

        context.AppUserPostComment.RemoveRange(comments);
    }

    public async Task DeleteCommentsByPostIdAsync(int postId)
    {
        var comments = await context.AppUserPostComment
            .Where(c => c.PostId == postId)
            .ToListAsync();

        context.AppUserPostComment.RemoveRange(comments);
    }

    public async Task DeleteCommentsByUserIdAsync(string appUserId)
    {
        var comments = await context.AppUserPostComment
            .Where(c => c.AppUserId == appUserId)
            .ToListAsync();

        context.AppUserPostComment.RemoveRange(comments);
    }

    public async Task<AppUserPostComment?> GetCommentByUserIdAsync(string appUserId, int commentId)
    {
        return await context.AppUserPostComment
            .Where(c => c.AppUserId == appUserId && c.Id == commentId)
            .FirstOrDefaultAsync();
    }
}
