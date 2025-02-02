using Application.Core;
using MediatR;

namespace Application.Users.DeleteUser;

public record DeleteUserCommand : IRequest<Result<Unit>>;
