using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenService
{
    Task<string> GetTokenAsync(AppUser user);
}
