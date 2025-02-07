using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.DeleteReport;

public class DeleteUserReportHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<DeleteUserReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteUserReportCommand request, CancellationToken cancellationToken)
    {
        var reporterId = userAccessor.GetCurrentUserId();

        var reportedUser = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (reportedUser is null) return null;

        var report = await unitOfWork.ReportsRepository.GetUserReportByIdAsync(reporterId, reportedUser.Id);

        if (report is null) return null;

        unitOfWork.ReportsRepository.DeleteUserReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete report.");
    }
}
