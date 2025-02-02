using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IRequest<Result<UserDto>>;
