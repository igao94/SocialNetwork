using Domain.Entities;

namespace Domain.Interfaces;

public interface ICommentsRepository
{
    void AddComment(AppUserPostComment comment);
    void RemoveComment(AppUserPostComment comment);
    Task<List<AppUserPostComment>> GetCommentsByPostIdsAsync(List<int> postIds);
    Task<List<AppUserPostComment>> GetCommentsByPostIdAsync(int postId);
    Task<List<AppUserPostComment>> GetCommentsByUserIdAsync(string appUserId);
    void RemoveComments(List<AppUserPostComment> comments);
    Task<AppUserPostComment?> GetCommentByUserIdAsync(string appUserId, int commentId);
}
