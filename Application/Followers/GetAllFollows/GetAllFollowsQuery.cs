﻿using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Followers.GetAllFollows;

public record GetAllFollowsQuery(FollowersParams FollowersParams) : IRequest<Result<PagedList<UserDto>>>;
