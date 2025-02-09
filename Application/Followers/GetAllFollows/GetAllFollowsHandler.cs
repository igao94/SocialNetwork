using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;

namespace Application.Followers.GetAllFollows;

public class GetAllFollowsHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<GetAllFollowsQuery, Result<PagedList<UserDto>>?>
{
    public async Task<Result<PagedList<UserDto>>?> Handle(GetAllFollowsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserByIdAsync(userAccessor.GetCurrentUserId());

        if (user is null) return null;

        var followersQuery = unitOfWork.FollowingsRepository.GetUserFollowersQuery(user.Id);

        var followingQuery = unitOfWork.FollowingsRepository.GetUserFollowingQuery(user.Id);

        var users = request.FollowersParams.Predicate.ToLower() switch
        {
            "followers" => await PagedList<UserDto>
                .CreateAsync(followersQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider),
                request.FollowersParams.PageNumber,
                request.FollowersParams.PageSize),

            "following" => await PagedList<UserDto>
                .CreateAsync(followingQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider),
                request.FollowersParams.PageNumber,
                request.FollowersParams.PageSize),

            _ => new PagedList<UserDto>([], 0, 1, 1)
        };

        return Result<PagedList<UserDto>>.Success(users);
    }
}
