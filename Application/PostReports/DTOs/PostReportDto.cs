namespace Application.PostReports.DTOs;

public class PostReportDto
{
    public string ReporterUsername { get; set; } = string.Empty;
    public int ReportedPostId { get; set; }
    public string ReportedPostUserUsername { get; set; } = string.Empty;
    public string Reason { get; set; } = null!;
    public DateTime CreationDate { get; set; }
}
