using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.AddReport;

public class AddUserReportHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<AddUserReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(AddUserReportCommand request, CancellationToken cancellationToken)
    {
        var reporter = await unitOfWork.UsersRepository.GetUserByIdAsync(userAccessor.GetCurrentUserId());

        if (reporter is null) return null;

        if (reporter.UserName == request.Username) return Result<Unit>.Failure("You can't report yourself.");

        var userToReport = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (userToReport is null) return null;

        var existingReport = await unitOfWork.ReportsRepository
            .GetUserReportByIdAsync(reporter.Id, userToReport.Id);

        if (existingReport is not null) return Result<Unit>.Failure("You have already reported this user.");

        var report = new UserReport
        {
            Reporter = reporter,
            ReporterId = reporter.Id,
            ReportedUser = userToReport,
            ReportedUserId = userToReport.Id,
            Reason = request.Reason
        };

        unitOfWork.ReportsRepository.AddUserReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to report user.");
    }
}
