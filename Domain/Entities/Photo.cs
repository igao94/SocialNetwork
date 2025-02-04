using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Photos")]
public class Photo
{
    public int PhotoId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public string PublicId { get; set; } = string.Empty;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}
