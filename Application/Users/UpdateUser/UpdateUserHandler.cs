using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.UpdateUser;

public class UpdateUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        mapper.Map(request, user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update user.");
    }
}
