using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostsRepository
{
    void CreatePost(AppUser user, Post post);
    void AddPhotoToPost(Post post, PostPhoto photo);
    Post? GetPostById(AppUser user, int postId);
    void DeletePost(AppUser user, Post post);
    IQueryable<Post> GetAllPostsQuery();
    IQueryable<Post> GetPostByIdQuery(int postId);
}
