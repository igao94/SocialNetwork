using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostsRepository
{
    Post? GetPostForUserById(AppUser user, int postId);
    void DeletePost(Post post);
    IQueryable<Post> GetAllPostsQuery();
    IQueryable<Post> GetPostByIdQuery(int postId);
    Task<Post?> GetPostByIdAsync(int postId);
    Task<Post?> GetPostWithPostPhotosByIdAsync(int postId);
    List<int> GetPostIds(AppUser user);
    List<PostPhoto> GetPostPhotos(AppUser user);
    Task<Post?> GetPostWithUsersByIdAsync(int postId);
}
