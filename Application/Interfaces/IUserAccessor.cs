namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetCurrentUserUsername();
    string GetCurrentUserId();
}
