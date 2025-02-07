using Application.Core;
using MediatR;

namespace Application.UserReports.DeleteReport;

public record DeleteReportCommand(string Username) : IRequest<Result<Unit>>;
