using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IAccountsRepository accountsRepository,
    IUsersRepository usersRepository) : IUnitOfWork
{
    public IAccountsRepository AccountRepository => accountsRepository;

    public IUsersRepository UsersRepository => usersRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
