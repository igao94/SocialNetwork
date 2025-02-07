﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<AppUserPostLike> AppUserPostLikes { get; set; }
    public DbSet<AppUserPostComment> AppUserPostComment { get; set; }
    public DbSet<AppUserFollowing> AppUserFollowings { get; set; }
    public DbSet<UserReport> UserReports { get; set; }
    public DbSet<PostReport> PostReports { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasMany(p => p.PostPhotos)
            .WithOne(pp => pp.Post)
            .HasForeignKey(pp => pp.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<AppUserPostLike>().HasKey(l => new { l.AppUserId, l.PostId });

        builder.Entity<AppUserPostLike>()
            .HasOne(l => l.AppUser)
            .WithMany(u => u.LikedPosts)
            .HasForeignKey(l => l.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUserPostLike>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUserPostComment>()
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUserPostComment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUserFollowing>().HasKey(uf => new { uf.ObserverId, uf.TargetId });

        builder.Entity<AppUserFollowing>()
            .HasOne(uf => uf.Observer)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.ObserverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUserFollowing>()
            .HasOne(uf => uf.Target)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.TargetId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UserReport>().HasKey(ur => new { ur.ReporterId, ur.ReportedUserId });

        builder.Entity<UserReport>()
            .HasOne(ur => ur.Reporter)
            .WithMany(u => u.UserReports)
            .HasForeignKey(ur => ur.ReporterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UserReport>()
            .HasOne(ur => ur.ReportedUser)
            .WithMany()
            .HasForeignKey(ur => ur.ReportedUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<PostReport>().HasKey(pp => new { pp.ReporterId, pp.ReportedPostId });

        builder.Entity<PostReport>()
            .HasOne(pp => pp.Reporter)
            .WithMany(u => u.PostReports)
            .HasForeignKey(pp => pp.ReporterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<PostReport>()
            .HasOne(pp => pp.ReportedPost)
            .WithMany(u => u.PostReports)
            .HasForeignKey(pp => pp.ReportedPostId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
