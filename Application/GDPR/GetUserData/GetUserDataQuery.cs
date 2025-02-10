using Application.Core;
using MediatR;

namespace Application.GDPR.GetUserData;

public record GetUserDataQuery : IRequest<Result<byte[]>>;
