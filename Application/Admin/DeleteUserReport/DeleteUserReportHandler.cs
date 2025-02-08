using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.DeleteUserReport;

public class DeleteUserReportHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteUserReportCommand request, CancellationToken cancellationToken)
    {
        var reporter = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.ReporterUsername);

        if (reporter is null) return null;

        var reporterUser = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.ReportedUserUsername);

        if (reporterUser is null) return null;

        var userReport = await unitOfWork.ReportsRepository.GetUserReportByIdAsync(reporter.Id, reporterUser.Id);

        if (userReport is null) return null;

        unitOfWork.ReportsRepository.DeleteUserReport(userReport);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user report.");
    }
}
