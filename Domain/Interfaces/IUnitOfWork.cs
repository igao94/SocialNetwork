namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IAccountsRepository AccountRepository { get; }
    IUsersRepository UsersRepository { get; }
    IPhotosRepository PhotosRepository { get; }
    IPostsRepository PostsRepository { get; }
    Task<bool> SaveChangesAsync();
}
