using Application.Core;
using Application.Interfaces;
using Application.PostReports.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.PostReports.GetAllPostReports;

public class GetAllPostReportsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllPostReportsQuery, Result<List<PostReportDto>>>
{
    public async Task<Result<List<PostReportDto>>> Handle(GetAllPostReportsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var postReports = await unitOfWork.ReportsRepository.GetAllPostReportsAsync(userId);

        return Result<List<PostReportDto>>.Success(mapper.Map<List<PostReportDto>>(postReports));
    }
}
