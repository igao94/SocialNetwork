using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.GetAllUsers;

public class GetAllUsersHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var usersQuery = unitOfWork.UsersRepository
            .GetAllUsersQuery(userAccessor.GetCurrentUserUsername(), request.SearchTerm);

        var users = await usersQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync();

        return Result<List<UserDto>>.Success(users);
    }
}
