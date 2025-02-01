using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IAccountsRepository accountsRepository) : IUnitOfWork
{
    public IAccountsRepository AccountRepository => accountsRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
