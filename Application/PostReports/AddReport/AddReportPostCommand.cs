using Application.Core;
using MediatR;

namespace Application.PostReports.AddReport;

public record AddReportPostCommand(int PostId, string Reason) : IRequest<Result<Unit>>;
