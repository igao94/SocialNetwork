using Application.Core;
using MediatR;

namespace Application.Users.UpdateUser;

public record UpdateUserCommand(string FirstName, 
    string LastName, 
    DateOnly DateOfBirth) : IRequest<Result<Unit>>;
