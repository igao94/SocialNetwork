using Application.Core;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Posts.UpdatePost;

public class UpdatePostCommand : IRequest<Result<Unit>>
{
    [JsonIgnore]
    public int PostId { get; set; }
    public string Content {  get; set; } = string.Empty;
}
