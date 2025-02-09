using Application.Core;
using Application.Interfaces;
using Application.UserReports.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.GetAllUserReports;

public class GetAllUserReportsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllUserReportsQuery, Result<PagedList<UserReportDto>>>
{
    public async Task<Result<PagedList<UserReportDto>>> Handle(GetAllUserReportsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var userReportsQuery = unitOfWork.ReportsRepository.GetUsersReportsForUserQuery(userId);

        var usersReports = await PagedList<UserReportDto>
            .CreateAsync(userReportsQuery.ProjectTo<UserReportDto>(mapper.ConfigurationProvider),
            request.UserReportsParams.PageNumber,
            request.UserReportsParams.PageSize);

        return Result<PagedList<UserReportDto>>.Success(usersReports);
    }
}
