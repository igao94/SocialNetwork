using Application.Core;
using Application.UserReports.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.GetAllUsersReports;

public class GetAllUsersReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersReportsQuery, Result<List<UserReportDto>>>
{
    public async Task<Result<List<UserReportDto>>> Handle(GetAllUsersReportsQuery request,
        CancellationToken cancellationToken)
    {
        var userReports = await unitOfWork.ReportsRepository.GetAllUsersReportsAsync();

        return Result<List<UserReportDto>>.Success(mapper.Map<List<UserReportDto>>(userReports));
    }
}
