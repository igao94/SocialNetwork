using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers.GetAllFollows;

public class GetAllFollowsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllFollowsQuery, Result<List<UserDto>>?>
{
    public async Task<Result<List<UserDto>>?> Handle(GetAllFollowsQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserByIdAsync(userAccessor.GetCurrentUserId());

        if (user is null) return null;

        List<UserDto> users = [];

        var followersQuery = unitOfWork.FollowingsRepository.GetUserFollowersQuery(user.Id);

        var followingQuery = unitOfWork.FollowingsRepository.GetUserFollowingQuery(user.Id);

        users = request.SearchTerm.ToLower() switch
        {
            "followers" => await followersQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync(),
            "following" => await followingQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync(),
            _ => users
        };

        return Result<List<UserDto>>.Success(users);
    }
}
