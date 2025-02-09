using Application.Core;

namespace Application.Likes;

public class LikesParams : PagingParams
{
    public int PostId { get; set; }
}
