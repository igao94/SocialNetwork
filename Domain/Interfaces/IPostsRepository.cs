using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostsRepository
{
    void CreatePost(AppUser user, Post post);
    void AddPhotoToPost(Post post, PostPhoto photo);
    Post? GetPostForUserById(AppUser user, int postId);
    void DeletePostForUser(AppUser user, Post post);
    void DeletePost(Post post);
    IQueryable<Post> GetAllPostsQuery();
    IQueryable<Post> GetPostByIdQuery(int postId);
    Task<Post?> GetPostByIdAsync(int postId);
    Task<Post?> GetPostWithPostPhotosByIdAsync(int postId);
    List<int> GetPostIds(AppUser user);
    List<PostPhoto> GetPostPhotos(AppUser user);
    Task<Post?> GetPostWithUsersByIdAsync(int postId);
}
