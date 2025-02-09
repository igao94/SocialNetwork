namespace Application.Admin.DTOs;

public class AdminUserReportDto
{
    public string ReporterUsername { get; set; } = string.Empty;
    public bool IsReporterActive { get; set; }
    public string ReportedUserUsername { get; set; } = string.Empty;
    public bool IsReportedUserActive { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreationDate { get; set; }

}
