using Application.Admin.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.GetAllUsersReports;

public class GetAllUsersReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersReportsQuery, Result<PagedList<AdminUserReportDto>>>
{
    public async Task<Result<PagedList<AdminUserReportDto>>> Handle(GetAllUsersReportsQuery request,
        CancellationToken cancellationToken)
    {
        var usersReportsQuery = unitOfWork.ReportsRepository.GetAllUsersReportForAdminQuery();

        var usersReports = await PagedList<AdminUserReportDto>
            .CreateAsync(usersReportsQuery.ProjectTo<AdminUserReportDto>(mapper.ConfigurationProvider),
            request.AdminUserReportsParams.PageNumber,
            request.AdminUserReportsParams.PageSize);

        return Result<PagedList<AdminUserReportDto>>.Success(usersReports);
    }
}
