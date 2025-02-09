using Application.Core;
using Application.PostReports.DTOs;
using MediatR;

namespace Application.PostReports.GetAllPostReports;

public record GetAllPostReportsQuery(PostReportsParams PostReportsParams) 
    : IRequest<Result<PagedList<PostReportDto>>>;    
