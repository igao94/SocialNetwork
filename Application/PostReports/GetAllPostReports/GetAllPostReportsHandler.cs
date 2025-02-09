using Application.Core;
using Application.Interfaces;
using Application.PostReports.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.PostReports.GetAllPostReports;

public class GetAllPostReportsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllPostReportsQuery, Result<PagedList<PostReportDto>>>
{
    public async Task<Result<PagedList<PostReportDto>>> Handle(GetAllPostReportsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var postsReportsQuery = unitOfWork.ReportsRepository.GetPostsReportsForUserQuery(userId);

        var postsReports = await PagedList<PostReportDto>
            .CreateAsync(postsReportsQuery.ProjectTo<PostReportDto>(mapper.ConfigurationProvider),
            request.PostReportsParams.PageNumber,
            request.PostReportsParams.PageSize);

        return Result<PagedList<PostReportDto>>.Success(postsReports);
    }
}
