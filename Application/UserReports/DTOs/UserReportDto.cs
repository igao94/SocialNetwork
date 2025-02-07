namespace Application.UserReports.DTOs;

public class UserReportDto
{
    public string ReporterUsername { get; set; } = string.Empty;
    public string ReportedUserUsername { get; set; } = string.Empty;
    public string Reason { get; set; } = null!;
    public DateTime CreationDate { get; set; }
}
