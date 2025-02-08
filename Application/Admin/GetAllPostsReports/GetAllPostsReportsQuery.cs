using Application.Core;
using Application.PostReports.DTOs;
using MediatR;

namespace Application.Admin.GetAllPostsReports;

public record GetAllPostsReportsQuery : IRequest<Result<List<PostReportDto>>>;
