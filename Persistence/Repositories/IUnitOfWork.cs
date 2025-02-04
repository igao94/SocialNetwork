using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IAccountsRepository accountsRepository,
    IUsersRepository usersRepository,
    IPhotosRepository photosRepository,
    IPostsRepository postsRepository) : IUnitOfWork
{
    public IAccountsRepository AccountRepository => accountsRepository;

    public IUsersRepository UsersRepository => usersRepository;

    public IPhotosRepository PhotosRepository => photosRepository;

    public IPostsRepository PostsRepository => postsRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
