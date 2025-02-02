using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.GetAllUsers;

public record GetAllUsersQuery(string? SearchTerm) : IRequest<Result<List<UserDto>>>;
 
