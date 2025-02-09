using Application.Admin.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.GetAllPostsReports;

public class GetAllPostsReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllPostsReportsQuery, Result<PagedList<AdminPostReportDto>>>
{
    public async Task<Result<PagedList<AdminPostReportDto>>> Handle(GetAllPostsReportsQuery request,
        CancellationToken cancellationToken)
    {
        var postsReportsQuery = unitOfWork.ReportsRepository.GetAllPostsReportsForAdminQuery();

        var postsReports = await PagedList<AdminPostReportDto>
            .CreateAsync(postsReportsQuery.ProjectTo<AdminPostReportDto>(mapper.ConfigurationProvider),
            request.AdminPostReportsParams.PageNumber,
            request.AdminPostReportsParams.PageSize);

        return Result<PagedList<AdminPostReportDto>>.Success(postsReports);
    }
}
