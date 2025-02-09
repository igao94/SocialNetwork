using Application.Admin.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.GetAllUsersReports;

public class GetAllUsersReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersReportsQuery, Result<List<AdminUserReportDto>>>
{
    public async Task<Result<List<AdminUserReportDto>>> Handle(GetAllUsersReportsQuery request,
        CancellationToken cancellationToken)
    {
        var usersReportsQuery = unitOfWork.ReportsRepository.GetAllUsersReportForAdminQuery();

        var usersReports = await usersReportsQuery
            .ProjectTo<AdminUserReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<AdminUserReportDto>>.Success(usersReports);
    }
}
