using Application.Core;
using MediatR;

namespace Application.UserReports.DeleteReport;

public record DeleteUserReportCommand(string Username) : IRequest<Result<Unit>>;
