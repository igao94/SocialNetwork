using Application.PostReports.AddReport;
using Application.PostReports.DeleteReport;
using Application.PostReports.GetAllPostReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostReportsController(IMediator mediator) : BaseApiController
{
    [HttpPost("report-post")]
    public async Task<IActionResult> ReportPost(AddReportPostCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        return HandleResult(await mediator.Send(new DeleteReportCommand(postId)));
    }

    [HttpGet]
    public async Task<IActionResult> GetPostReports()
    {
        return HandleResult(await mediator.Send(new GetAllPostReportsQuery()));
    }
}
