using Domain.Entities;

namespace Domain.Interfaces;

public interface ICommentsRepository
{
    void AddComment(AppUserPostComment comment);
    void RemoveComment(AppUserPostComment comment);
    Task DeleteCommentsByPostIdsAsync(List<int> postIds);
    Task DeleteCommentsByPostIdAsync(int postId);
    Task DeleteCommentsByUserIdAsync(string appUserId);
    Task<AppUserPostComment?> GetCommentByUserIdAsync(string appUserId, int commentId);
    IQueryable<AppUserPostComment> GetAllCommentsQuery(string appUserId);
}
