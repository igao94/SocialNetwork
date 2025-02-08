using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class PostsRepository(DataContext context) : IPostsRepository
{
    public void CreatePost(AppUser user, Post post) => user.Posts.Add(post);

    public void AddPhotoToPost(Post post, PostPhoto photo) => post.PostPhotos.Add(photo);

    public Post? GetPostForUserById(AppUser user, int postId)
    {
        return user.Posts.FirstOrDefault(p => p.PostId == postId);
    }

    public void DeletePostForUser(AppUser user, Post post) => user.Posts.Remove(post);

    public void DeletePost(Post post) => context.Posts.Remove(post);

    public IQueryable<Post> GetAllPostsQuery()
    {
        IQueryable<Post> query = context.Posts.OrderByDescending(p => p.CreationDate);

        return query;
    }

    public IQueryable<Post> GetPostByIdQuery(int postId)
    {
        IQueryable<Post> query = context.Posts.Where(p => p.PostId == postId);

        return query;
    }

    public async Task<Post?> GetPostByIdAsync(int postId) => await context.Posts.FindAsync(postId);

    public async Task<Post?> GetPostWithPostPhotosByIdAsync(int postId)
    {
        return await context.Posts
            .Include(p => p.PostPhotos)
            .FirstOrDefaultAsync(u => u.PostId == postId);
    }

    public async Task<Post?> GetPostWithUsersByIdAsync(int postId)
    {
        return await context.Posts
            .Include(u => u.AppUser)
            .FirstOrDefaultAsync(u => u.PostId == postId);
    }

    public List<int> GetPostIds(AppUser user) => user.Posts.Select(p => p.PostId).ToList();

    public List<PostPhoto> GetPostPhotos(AppUser user) => user.Posts.SelectMany(p => p.PostPhotos).ToList();
}
