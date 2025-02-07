using Application.Core;
using Application.PostReports.DTOs;
using MediatR;

namespace Application.PostReports.GetAllPostReports;

public record GetAllPostReportsQuery : IRequest<Result<List<PostReportDto>>>;    
