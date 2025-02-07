using FluentValidation;

namespace Application.UserReports.AddReport;

public class ReportUserValidator : AbstractValidator<AddUserReportCommand>
{
    public ReportUserValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .WithMessage("Please enter the username of the user you wish to report.");        
        
        RuleFor(r => r.Reason)
            .NotEmpty()
            .WithMessage("Please provide a reason for reporting the user.");
    }
}
