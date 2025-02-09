using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.GetAllUsers;

public record GetAllUsersQuery(UsersParams UsersParams) : IRequest<Result<PagedList<UserDto>>>;
 
