﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class FollowingsRepository(DataContext context) : IFollowingsRepository
{
    public async Task<AppUserFollowing?> GetFollowingAsync(string observerId, string targetId)
    {
        return await context.AppUserFollowings.FindAsync(observerId, targetId);
    }

    public void AddFollowing(AppUserFollowing userFollowing) => context.AppUserFollowings.Add(userFollowing);

    public void RemoveFollowing(AppUserFollowing userFollowing)
    {
        context.AppUserFollowings.Remove(userFollowing);
    }

    public IQueryable<AppUser> GetUserFollowersQuery(string appUserId)
    {
        IQueryable<AppUser> query = context.AppUserFollowings
            .Where(u => u.TargetId == appUserId)
            .Select(u => u.Observer);

        return query;
    }

    public IQueryable<AppUser> GetUserFollowingQuery(string appUserId)
    {
        IQueryable<AppUser> query = context.AppUserFollowings
            .Where(u => u.ObserverId == appUserId)
            .Select(u => u.Target);

        return query;
    }

    public async Task RemoveAllFollowsForUserAsync(string appUserId)
    {
        var follows = await context.AppUserFollowings
            .Where(u => u.ObserverId == appUserId || u.TargetId == appUserId)
            .ToListAsync();

        context.AppUserFollowings.RemoveRange(follows);
    }

    public IQueryable<Post> GetPostsFromFollowedUsersQuery(string appUserId)
    {
        IQueryable<Post> query = context.Posts
            .Where(p => context.AppUserFollowings
                .Where(uf => uf.ObserverId == appUserId)
                .Select(uf => uf.TargetId)
                .Contains(p.AppUserId))
            .OrderByDescending(p => p.CreationDate);

        return query;

    }

    public IQueryable<AppUserFollowing> GetAllUserFollowsQuery(string appUserId)
    {
        return context.AppUserFollowings
            .Where(u => u.ObserverId == appUserId || u.TargetId == appUserId);
    }
}
