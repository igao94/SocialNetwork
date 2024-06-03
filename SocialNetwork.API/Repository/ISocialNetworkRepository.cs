using SocialNetwork.API.Models.Entites;

namespace SocialNetwork.API.Repository
{
    public interface ISocialNetworkRepository
    {
        Task<User?> GetUserByCredentialsAsync(string username, string password);
        Task<User?> GetUserByEmailAsync(string email);
        Task<string> CreateTokenAsync(string email);
        Task<int> GetUserIdByTokenAsync(string token);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserAsync(int userId);
        Task<User?> SetPasswordAsync(int userId, string password);
        Task<IEnumerable<User>> GetUsersAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<bool> UserEmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<bool> UserExistsAsync(int userId);
        Task<IEnumerable<Post>> GetPostsAsync(int userId);
        Task<Post?> GetPostAsync(int userId, int postId);
        Task<Post?> GetPostByIdAsync(int postId);
        Task AddPostAsync(Post post);
        Task DeletePostAsync(Post post);
        Task AddCommentAsync(Comment comment);
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task DeleteCommentAsync(Comment comment);
        Task AddLikeAsync(Like like);
        Task<Like?> GetLikeByIdAsync(int likeId);
        Task DeleteLikeAsync(Like like);
        Task<bool> SaveChangesAsync();
        Task<Connection?> GetConnectionAsync(int firstUserId, int secondUserId);
        Task AddConnectionAsync(int firstUserId, int secondUserId);
        Task<IEnumerable<Connection>> GetConnectionsAsync(int userId);
        Task DeleteConnectionAsync(Connection connection);
        Task<ReportUser?> GetReportedUserAsync(int reportedUserId);
        Task AddUserReportAsync(ReportUser reportUser);
        Task<IEnumerable<ReportUser>> GetReportedUsersAsync();
        Task DeleteUserReportAsync(ReportUser reportUser);
        Task<ReportPost?> GetReportedPostAsync(int reportedPostId);
        Task AddPostReportAsync(ReportPost reportPost);
        Task<IEnumerable<ReportPost>> GetReportedPostsAsync();
        Task DeletePostReportAsync(ReportPost reportPost);
        Task<IEnumerable<Comment>> GetCommentsAsync(int userId);
        Task<IEnumerable<Like>> GetLikesAsync(int userId);
    }
}
