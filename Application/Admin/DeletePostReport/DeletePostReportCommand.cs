using Application.Core;
using MediatR;

namespace Application.Admin.DeletePostReport;

public record DeletePostReportCommand(string ReporterUserName, int ReportedPostId) : IRequest<Result<Unit>>;
