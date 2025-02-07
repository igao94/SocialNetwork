using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserReportsRepository
{
    Task<UserReport?> GetUserReportByIdAsync(string reporterId, string reportedUserId);
    void AddReport(UserReport reportUser);
    void DeleteReport(UserReport userReport);
    Task DeleteAllReportsAsync(string appUserId);
}
