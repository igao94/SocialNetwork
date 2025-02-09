using Application.Admin.DTOs;
using Application.Core;
using MediatR;

namespace Application.Admin.GetAllUsersReports;

public record GetAllUsersReportsQuery : IRequest<Result<List<AdminUserReportDto>>>;
