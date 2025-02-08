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
    public async Task<IActionResult> GetAllUsersReports()
    {
        return HandleResult(await mediator.Send(new GetAllUsersReportsQuery()));
    }

    [HttpGet("posts-reports")]
    public async Task<IActionResult> GetAllPostsReports()
    {
        return HandleResult(await mediator.Send(new GetAllPostsReportsQuery()));
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
}
