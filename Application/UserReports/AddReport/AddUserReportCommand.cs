using Application.Core;
using MediatR;

namespace Application.UserReports.AddReport;

public record AddUserReportCommand(string Username, string Reason) : IRequest<Result<Unit>>;
