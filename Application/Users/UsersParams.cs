using Application.Core;

namespace Application.Users;

public class UsersParams : PagingParams
{
    public string? SearchTerm { get; set; }
}
