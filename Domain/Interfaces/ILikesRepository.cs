using Domain.Entities;

namespace Domain.Interfaces;

public interface ILikesRepository
{
    Task<AppUserPostLike?> GetLikeByIdAsync(string appUserId, int postId);
    void AddLike(AppUserPostLike like);
    void RemoveLike(AppUserPostLike like);
    Task<List<AppUserPostLike>> GetLikesByPostIdAsync(int postId);
    Task<List<AppUserPostLike>> GetLikesByPostIdsAsync(List<int> postIds);
    Task<List<AppUserPostLike>> GetLikesByUserIdAsync(string appUserId);
    Task<List<AppUser>> GetUsersWhoLikedPostAsync(int postId);
    void RemoveLikes(List<AppUserPostLike> likes);
}
