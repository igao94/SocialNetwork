using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.GetUserByUsername;

public class GetUserByUsernameHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetUserByUsernameQuery, Result<UserDto>?>
{
    public async Task<Result<UserDto>?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var currentUserUsername = userAccessor.GetCurrentUserUsername();

        if (currentUserUsername == request.Username) return null;

        var userQuery = unitOfWork.UsersRepository.GetUserByUsernameQuery(request.Username);

        var user = await userQuery
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return user is null ? null : Result<UserDto>.Success(user);
    }
}