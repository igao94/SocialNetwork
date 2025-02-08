using Application.Core;
using MediatR;

namespace Application.Admin.DeleteUserAsAdmin;

public record DeleteUserAsAdminCommand(string Username) : IRequest<Result<Unit>>;
