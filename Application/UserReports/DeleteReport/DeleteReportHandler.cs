using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.UserReports.DeleteReport;

public class DeleteReportHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<DeleteReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var reporterId = userAccessor.GetCurrentUserId();

        var reportedUser = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (reportedUser is null) return null;

        var report = await unitOfWork.UserReportsRepository.GetUserReportByIdAsync(reporterId, reportedUser.Id);

        if (report is null) return null;

        unitOfWork.UserReportsRepository.DeleteReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete report.");
    }
}
