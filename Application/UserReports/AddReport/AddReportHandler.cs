using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.AddReport;

public class AddReportHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<AddReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(AddReportCommand request, CancellationToken cancellationToken)
    {
        var reporter = await unitOfWork.UsersRepository.GetUserByIdAsync(userAccessor.GetCurrentUserId());

        if (reporter is null) return null;

        if (reporter.UserName == request.Username) return Result<Unit>.Failure("You can't report yourself.");

        var userToReport = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (userToReport is null) return null;

        var existingReport = await unitOfWork.UserReportsRepository
            .GetUserReportByIdAsync(reporter.Id, userToReport.Id);

        if (existingReport is not null) return Result<Unit>.Failure("You have already reported this user.");

        var report = new UserReport
        {
            Reporter = reporter,
            ReporterId = reporter.Id,
            ReportredUser = userToReport,
            ReportedUserId = userToReport.Id,
            Reason = request.Reason
        };

        unitOfWork.UserReportsRepository.AddReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to report user.");
    }
}
