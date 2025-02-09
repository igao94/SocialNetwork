using Application.Admin.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.GetAllPostsReports;

public class GetAllPostsReportsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllPostsReportsQuery, Result<List<AdminPostReportDto>>>
{
    public async Task<Result<List<AdminPostReportDto>>> Handle(GetAllPostsReportsQuery request,
        CancellationToken cancellationToken)
    {
        var postsReportsQuery = unitOfWork.ReportsRepository.GetAllPostsReportsForAdminQuery();

        var postsReports = await postsReportsQuery
            .ProjectTo<AdminPostReportDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<AdminPostReportDto>>.Success(postsReports);
    }
}
