using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.DeletePostReport;

public class DeletePostReportHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePostReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeletePostReportCommand request, CancellationToken cancellationToken)
    {
        var reporter = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.ReporterUserName);

        if (reporter is null) return null;

        var postReport = await unitOfWork.ReportsRepository
            .GetPostReportByIdAsync(reporter.Id, request.ReportedPostId);

        if (postReport is null) return null;

        unitOfWork.ReportsRepository.DeletePostReport(postReport);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete post report.");
    }
}
