using Application.Core;
using MediatR;

namespace Application.Admin.DeletePostAsAdmin;

public record DeletePostAsAdminCommand(int PostId) : IRequest<Result<Unit>>;
