using Application.Core;
using MediatR;

namespace Application.UserReports.AddReport;

public record AddReportCommand(string Username, string Reason) : IRequest<Result<Unit>>;
