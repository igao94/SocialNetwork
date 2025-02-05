using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<AppUserPostLike> AppUserPostLikes { get; set; }

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
    }
}
