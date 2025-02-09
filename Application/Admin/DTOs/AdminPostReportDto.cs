namespace Application.Admin.DTOs;

public class AdminPostReportDto
{
    public string ReporterUsername { get; set; } = string.Empty;
    public bool IsReporterActive { get; set; }
    public int ReportedPostId { get; set; }
    public string ReportedPostUserUsername { get; set; } = string.Empty;
    public bool IsReporterPostUserActive { get; set; }
    public string Reason { get; set; } = null!;
    public DateTime CreationDate { get; set; }
}
