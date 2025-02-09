using Domain.Entities;

namespace Domain.Interfaces;

public interface IReportsRepository
{
    Task<UserReport?> GetUserReportByIdAsync(string reporterId, string reportedUserId);
    void AddUserReport(UserReport reportUser);
    void DeleteUserReport(UserReport userReport);
    Task DeleteAllUserReportsAsync(string appUserId);
    Task<PostReport?> GetPostReportByIdAsync(string reporterId, int postId);
    void AddPostReport(PostReport postReport);
    void DeletePostReport(PostReport postReport);
    Task DeletePostReportsAsync(int postId);
    Task<List<UserReport>> GetUsersReportsForUserAsync(string appUserId);
    Task<List<PostReport>> GetPostsReportsForUserAsync(string appUserId);
    Task DeleteAllPostsReportsForUserAsync(string appUserId);
    IQueryable<UserReport> GetAllUsersReportForAdminQuery();
    IQueryable<PostReport> GetAllPostsReportsForAdminQuery();
}
