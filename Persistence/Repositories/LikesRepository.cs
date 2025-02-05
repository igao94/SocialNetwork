using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class LikesRepository(DataContext context) : ILikesRepository
{
    public async Task<AppUserPostLike?> GetLikeByIdAsync(string appUserId, int postId)
    {
        return await context.AppUserPostLikes.FindAsync(appUserId, postId);
    }

    public void AddLike(AppUserPostLike like) => context.AppUserPostLikes.Add(like);

    public void RemoveLike(AppUserPostLike like) => context.AppUserPostLikes.Remove(like);

    public void RemoveLikes(List<AppUserPostLike> likes) => context.AppUserPostLikes.RemoveRange(likes);

    public async Task<List<AppUserPostLike>> GetLikesByPostIdsAsync(List<int> postIds)
    {
        return await context.AppUserPostLikes
            .Where(l => postIds.Contains(l.PostId))
            .ToListAsync();
    }

    public async Task<List<AppUserPostLike>> GetLikesByPostIdAsync(int postId)
    {
        return await context.AppUserPostLikes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }

    public async Task<List<AppUserPostLike>> GetLikesByUserIdAsync(string appUserId)
    {
        return await context.AppUserPostLikes
            .Where(l => l.AppUserId == appUserId)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetUsersWhoLikedPostAsync(int postId)
    {
        return await context.AppUserPostLikes
            .Where(l => l.PostId == postId)
            .Select(l => new AppUser
            {
                FirstName = l.AppUser.FirstName,
                LastName = l.AppUser.LastName,
                UserName = l.AppUser.UserName,
                Photos = l.AppUser.Photos
            })
            .ToListAsync();
    }
}
