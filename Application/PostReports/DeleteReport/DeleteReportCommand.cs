using Application.Core;
using MediatR;

namespace Application.PostReports.DeleteReport;

public record DeleteReportCommand(int PostId) : IRequest<Result<Unit>>;
