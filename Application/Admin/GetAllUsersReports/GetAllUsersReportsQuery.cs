using Application.Core;
using Application.UserReports.DTOs;
using MediatR;

namespace Application.Admin.GetAllUsersReports;

public record GetAllUsersReportsQuery : IRequest<Result<List<UserReportDto>>>;
