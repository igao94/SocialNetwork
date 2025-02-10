using Domain.Entities;

namespace Domain.Interfaces;

public interface IFollowingsRepository
{
    Task<AppUserFollowing?> GetFollowingAsync(string observerId, string targetId);
    void AddFollowing(AppUserFollowing userFollowing);
    void RemoveFollowing(AppUserFollowing userFollowing);
    public IQueryable<AppUser> GetUserFollowersQuery(string appUserId);
    public IQueryable<AppUser> GetUserFollowingQuery(string appUserId);
    Task RemoveAllFollowsForUserAsync(string appUserId);
    IQueryable<Post> GetPostsFromFollowedUsersQuery(string appUserId);
    IQueryable<AppUserFollowing> GetAllUserFollowsQuery(string appUserId);
}
