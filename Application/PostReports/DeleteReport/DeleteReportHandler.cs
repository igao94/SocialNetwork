using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.PostReports.DeleteReport;

public class DeleteReportHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<DeleteReportCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetCurrentUserId();

        var post = await unitOfWork.PostsRepository.GetPostByIdAsync(request.PostId);

        if (post is null) return null;

        var report = await unitOfWork.ReportsRepository.GetPostReportByIdAsync(userId, post.PostId);

        if (report is null) return null;

        unitOfWork.ReportsRepository.DeletePostReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete report.");
    }
}
