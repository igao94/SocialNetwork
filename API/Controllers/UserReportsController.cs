using Application.UserReports.AddReport;
using Application.UserReports.DeleteReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserReportsController(IMediator mediator) : BaseApiController
{
    [HttpPost("reportUser")]
    public async Task<IActionResult> ReportUser(AddReportCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteReport(string username)
    {
        return HandleResult(await mediator.Send(new DeleteReportCommand(username)));
    }
}
