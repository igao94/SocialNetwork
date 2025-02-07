using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.PostReports.AddReport;

public class AddReportPostHadler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<AddReportPostCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(AddReportPostCommand request, CancellationToken cancellationToken)
    {
        var reporter = await unitOfWork.UsersRepository
            .GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (reporter is null) return null;

        var post = await unitOfWork.PostsRepository.GetPostWithUsersByIdAsync(request.PostId);

        if (post is null) return null;

        if (reporter.UserName == post.AppUser.UserName) 
            return Result<Unit>.Failure("You can't report your own posts.");

        var existingReport = await unitOfWork.ReportsRepository.GetPostReportByIdAsync(reporter.Id, post.PostId);

        if (existingReport is not null) return Result<Unit>.Failure("You have already reported this post.");

        var report = new PostReport
        {
            Reporter = reporter,
            ReporterId = reporter.Id,
            ReportedPost = post,
            ReportedPostId = post.PostId,
            Reason = request.Reason
        };

        unitOfWork.ReportsRepository.AddPostReport(report);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to report post.");
    }
}
