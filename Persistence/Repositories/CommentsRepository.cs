using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CommentsRepository(DataContext context) : ICommentsRepository
{
    public void AddComment(AppUserPostComment comment) => context.AppUserPostComment.Add(comment);

    public void RemoveComment(AppUserPostComment comment) => context.AppUserPostComment.Remove(comment);

    public void RemoveComments(List<AppUserPostComment> comments)
    {
        context.AppUserPostComment.RemoveRange(comments);
    }

    public async Task<List<AppUserPostComment>> GetCommentsByPostIdsAsync(List<int> postIds)
    {
        return await context.AppUserPostComment
            .Where(c => postIds.Contains(c.PostId))
            .ToListAsync();
    }

    public async Task<List<AppUserPostComment>> GetCommentsByPostIdAsync(int postId)
    {
        return await context.AppUserPostComment
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<List<AppUserPostComment>> GetCommentsByUserIdAsync(string appUserId)
    {
        return await context.AppUserPostComment
            .Where(c => c.AppUserId == appUserId)
            .ToListAsync();
    }

    public async Task<AppUserPostComment?> GetCommentByUserIdAsync(string appUserId, int commentId)
    {
        return await context.AppUserPostComment
            .Where(c => c.AppUserId == appUserId && c.Id == commentId)
            .FirstOrDefaultAsync();
    }
}
