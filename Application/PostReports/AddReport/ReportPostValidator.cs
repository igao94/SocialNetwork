using FluentValidation;

namespace Application.PostReports.AddReport;

public class ReportPostValidator : AbstractValidator<AddReportPostCommand>
{
    public ReportPostValidator()
    {
        RuleFor(r => r.PostId)
            .NotEmpty()
            .WithMessage("Please enter the Id of the post you wish to report.");

        RuleFor(r => r.Reason)
            .NotEmpty()
            .WithMessage("Please provide a reason for reporting the post.");
    }
}
