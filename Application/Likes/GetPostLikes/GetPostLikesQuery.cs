using Application.Core;
using Application.Likes.DTOs;
using MediatR;

namespace Application.Likes.GetPostLikes;

public record GetPostLikesQuery(int PostId) : IRequest<Result<List<UserLikeDto>>>;
