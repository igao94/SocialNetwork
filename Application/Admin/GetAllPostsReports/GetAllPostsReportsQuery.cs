using Application.Admin.DTOs;
using Application.Core;
using MediatR;

namespace Application.Admin.GetAllPostsReports;

public record GetAllPostsReportsQuery(AdminPostReportsParams AdminPostReportsParams) 
    : IRequest<Result<PagedList<AdminPostReportDto>>>;
