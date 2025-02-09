using Application.Core;

namespace Application.Followers;

public class FollowersParams : PagingParams
{
    public string Predicate {  get; set; } = string.Empty;
}
