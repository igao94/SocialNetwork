﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class ReportsRepository(DataContext context) : IReportsRepository
{
    public async Task<UserReport?> GetUserReportByIdAsync(string reporterId, string reportedUserId)
    {
        return await context.UserReports.FindAsync(reporterId, reportedUserId);
    }

    public void AddUserReport(UserReport userReport) => context.UserReports.Add(userReport);

    public void DeleteUserReport(UserReport userReport) => context.UserReports.Remove(userReport);

    public async Task DeleteAllUserReportsAsync(string appUserId)
    {
        var reports = await context.UserReports
            .Where(u => u.ReporterId == appUserId || u.ReportedUserId == appUserId)
            .ToListAsync();

        context.UserReports.RemoveRange(reports);
    }

    public async Task<PostReport?> GetPostReportByIdAsync(string reporterId, int postId)
    {
        return await context.PostReports.FindAsync(reporterId, postId);
    }

    public void AddPostReport(PostReport postReport) => context.PostReports.Add(postReport);

    public void DeletePostReport(PostReport postReport) => context.PostReports.Remove(postReport);

    public async Task DeleteAllPostsReportsForUserAsync(string appUserId)
    {
        var postReports = await context.PostReports
            .Where(pr => pr.ReporterId == appUserId || pr.ReportedPost.AppUserId == appUserId)
            .ToListAsync();

        context.PostReports.RemoveRange(postReports);
    }

    public async Task DeletePostReportsAsync(int postId)
    {
        var postReports = await context.PostReports
            .Where(p => p.ReportedPostId == postId)
            .ToListAsync();

        context.PostReports.RemoveRange(postReports);
    }

    public IQueryable<UserReport> GetUsersReportsForUserQuery(string appUserId)
    {
        return context.UserReports
            .OrderByDescending(u => u.CreationDate)
            .Where(u => u.ReporterId == appUserId);
    }

    public IQueryable<PostReport> GetPostsReportsForUserQuery(string appUserId)
    {
        return context.PostReports
            .OrderByDescending(u => u.CreationDate)
            .Where(u => u.ReporterId == appUserId);
    }

    public IQueryable<UserReport> GetAllUsersReportForAdminQuery()
    {
        return context.UserReports
            .IgnoreQueryFilters()
            .OrderByDescending(ur => ur.CreationDate);
    }

    public IQueryable<PostReport> GetAllPostsReportsForAdminQuery()
    {
        return context.PostReports
            .IgnoreQueryFilters()
            .OrderByDescending(ur => ur.CreationDate);
    }
}
