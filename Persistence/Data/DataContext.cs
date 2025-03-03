using Domain.Entities;
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

        builder.Entity<AppUser>(x =>
        {
            x.HasQueryFilter(u => u.IsActive);

            x.HasIndex(u => u.UserName).IsUnique();

            x.HasIndex(u => u.Email).IsUnique();
        });

        builder.Entity<Photo>().HasQueryFilter(p => p.AppUser.IsActive);

        builder.Entity<Post>(x =>
        {
            x.HasQueryFilter(p => p.AppUser.IsActive);

            x.HasMany(p => p.PostPhotos)
                .WithOne(pp => pp.Post)
                .HasForeignKey(pp => pp.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PostPhoto>().HasQueryFilter(pp => pp.Post.AppUser.IsActive);

        builder.Entity<AppUserPostLike>(x =>
        {
            x.HasKey(l => new { l.AppUserId, l.PostId });

            x.HasQueryFilter(l => l.AppUser.IsActive && l.Post.AppUser.IsActive);

            x.HasOne(l => l.AppUser)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<AppUserPostComment>(x =>
        {
            x.HasQueryFilter(c => c.AppUser.IsActive && c.Post.AppUser.IsActive);


            x.HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<AppUserFollowing>(x =>
        {
            x.HasKey(uf => new { uf.ObserverId, uf.TargetId });

            x.HasQueryFilter(uf => uf.Observer.IsActive && uf.Target.IsActive);

            x.HasOne(uf => uf.Observer)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.ObserverId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(uf => uf.Target)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.TargetId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<UserReport>(x =>
        {
            x.HasKey(ur => new { ur.ReporterId, ur.ReportedUserId });

            x.HasQueryFilter(ur => ur.Reporter.IsActive && ur.ReportedUser.IsActive);

            x.HasOne(ur => ur.Reporter)
                .WithMany(u => u.ReportsMade)
                .HasForeignKey(ur => ur.ReporterId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(ur => ur.ReportedUser)
                .WithMany(u => u.ReportsReceived)
                .HasForeignKey(ur => ur.ReportedUserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<PostReport>(x =>
        {
            x.HasKey(pr => new { pr.ReporterId, pr.ReportedPostId });

            x.HasQueryFilter(pr => pr.Reporter.IsActive && pr.ReportedPost.AppUser.IsActive);

            x.HasOne(pr => pr.Reporter)
                .WithMany(u => u.ReportedPosts)
                .HasForeignKey(pr => pr.ReporterId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(pr => pr.ReportedPost)
                .WithMany(p => p.PostReports)
                .HasForeignKey(pr => pr.ReportedPostId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
