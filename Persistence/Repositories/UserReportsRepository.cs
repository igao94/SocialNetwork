using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class UserReportsRepository(DataContext context) : IUserReportsRepository
{
    public async Task<UserReport?> GetUserReportByIdAsync(string reporterId, string reportedUserId)
    {
        return await context.UserReports.FindAsync(reporterId, reportedUserId);
    }

    public void AddReport(UserReport userReport) => context.UserReports.Add(userReport);

    public void DeleteReport(UserReport userReport) => context.UserReports.Remove(userReport);

    public async Task DeleteAllReportsAsync(string appUserId)
    {
        var reports = await context.UserReports
            .Where(u => u.ReporterId == appUserId || u.ReportedUserId == appUserId)
            .ToListAsync();

        context.UserReports.RemoveRange(reports);
    }
}
