using Application.UserReports.AddReport;
using Application.UserReports.DeleteReport;
using Application.UserReports.GetAllUserReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserReportsController(IMediator mediator) : BaseApiController
{
    [HttpPost("reportUser")]
    public async Task<IActionResult> ReportUser(AddUserReportCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete("{reportedUserUsername}")]
    public async Task<IActionResult> DeleteReport(string reportedUserUsername)
    {
        return HandleResult(await mediator.Send(new DeleteUserReportCommand(reportedUserUsername)));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserReports()
    {
        return HandleResult(await mediator.Send(new GetAllUserReportsQuery()));
    }
}
