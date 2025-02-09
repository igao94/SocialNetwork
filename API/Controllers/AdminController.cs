using Application.Admin;
using Application.Admin.ActivateUserAccount;
using Application.Admin.DeactivateUserAccount;
using Application.Admin.DeletePostAsAdmin;
using Application.Admin.DeletePostReport;
using Application.Admin.DeleteUserAsAdmin;
using Application.Admin.DeleteUserReport;
using Application.Admin.GetAllPostsReports;
using Application.Admin.GetAllUsersReports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Authorization.Constants;

namespace API.Controllers;

[Authorize(Policy = PolicyTypes.RequireAdminRole)]
public class AdminController(IMediator mediator) : BaseApiController
{
    [HttpGet("users-reports")]
    public async Task<IActionResult> GetAllUsersReports
        ([FromQuery] AdminUserReportsParams adminUserReportsParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllUsersReportsQuery(adminUserReportsParams)));
    }

    [HttpGet("posts-reports")]
    public async Task<IActionResult> GetAllPostsReports
        ([FromQuery] AdminPostReportsParams adminPostReportsParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllPostsReportsQuery(adminPostReportsParams)));
    }

    [HttpDelete("delete-user-report/{reporterUsername}/{reportedUserUsername}")]
    public async Task<IActionResult> DeleteUserReport(string reporterUsername, string reportedUserUsername)
    {
        return HandleResult(await mediator
            .Send(new DeleteUserReportCommand(reporterUsername, reportedUserUsername)));
    }

    [HttpDelete("delete-post-report/{reporterUsername}/{reportedPostId}")]
    public async Task<IActionResult> DeletePostReport(string reporterUsername, int reportedPostId)
    {
        return HandleResult(await mediator.Send(new DeletePostReportCommand(reporterUsername, reportedPostId)));
    }

    [HttpDelete("delete-user/{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        return HandleResult(await mediator.Send(new DeleteUserAsAdminCommand(username)));
    }

    [HttpDelete("delete-post/{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        return HandleResult(await mediator.Send(new DeletePostAsAdminCommand(postId)));
    }

    [HttpPut("deactivate-user-account/{username}")]
    public async Task<IActionResult> DeactivateUserAccount(string username)
    {
        return HandleResult(await mediator.Send(new DeactivateUserAccountCommand(username)));
    }

    [HttpPut("activate-user-account{username}")]
    public async Task<IActionResult> ActivateUserAccount(string username)
    {
        return HandleResult(await mediator.Send(new ActivateUserAccountCommand(username)));
    }
}
