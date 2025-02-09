using Application.Core;
using Application.Likes.DTOs;
using MediatR;

namespace Application.Likes.GetPostLikes;

public record GetPostLikesQuery(LikesParams LikesParams) : IRequest<Result<PagedList<UserLikeDto>>>;
