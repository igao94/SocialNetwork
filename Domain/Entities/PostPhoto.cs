using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("PostPhotos")]
public class PostPhoto
{
    public int PostPhotoId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
