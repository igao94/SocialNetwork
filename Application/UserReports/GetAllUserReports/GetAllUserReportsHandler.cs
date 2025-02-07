using Application.Core;
using Application.Interfaces;
using Application.UserReports.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.GetAllUserReports;

public class GetAllUserReportsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllUserReportsQuery, Result<List<UserReportDto>>>
{
    public async Task<Result<List<UserReportDto>>> Handle(GetAllUserReportsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var userReports = await unitOfWork.ReportsRepository.GetAllUserReportsAsync(userId);

        return Result<List<UserReportDto>>.Success(mapper.Map<List<UserReportDto>>(userReports));
    }
}
