using Application.Core;
using MediatR;

namespace Application.Admin.DeleteUserReport;

public record DeleteUserReportCommand(string ReporterUsername, string ReportedUserUsername)
    : IRequest<Result<Unit>>;
