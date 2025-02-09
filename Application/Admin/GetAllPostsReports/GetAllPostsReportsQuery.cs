using Application.Admin.DTOs;
using Application.Core;
using MediatR;

namespace Application.Admin.GetAllPostsReports;

public record GetAllPostsReportsQuery : IRequest<Result<List<AdminPostReportDto>>>;
