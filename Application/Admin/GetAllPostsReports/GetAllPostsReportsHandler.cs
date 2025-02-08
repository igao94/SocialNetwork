using Application.Core;
using Application.PostReports.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.GetAllPostsReports;

public class GetAllPostsReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllPostsReportsQuery, Result<List<PostReportDto>>>
{
    public async Task<Result<List<PostReportDto>>> Handle(GetAllPostsReportsQuery request,
        CancellationToken cancellationToken)
    {
        var postsReports = await unitOfWork.ReportsRepository.GetAllPostReportsAsync();

        return Result<List<PostReportDto>>.Success(mapper.Map<List<PostReportDto>>(postsReports));
    }
}
