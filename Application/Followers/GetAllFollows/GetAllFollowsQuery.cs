using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Followers.GetAllFollows;

public record GetAllFollowsQuery(string SearchTerm) : IRequest<Result<List<UserDto>>>;
