using Domain.Entities;

namespace Domain.Interfaces;

public interface ILikesRepository
{
    Task<AppUserPostLike?> GetLikeByIdAsync(string appUserId, int postId);
    void AddLike(AppUserPostLike like);
    void RemoveLike(AppUserPostLike like);
    Task RemovePostLikesAsync(int postId);
    Task RemovePostsLikesAsync(List<int> postIds);
    Task<List<AppUser>> GetUsersWhoLikedPostAsync(int postId);
    Task RemoveUserLikesAsync(string appUserId);
}
