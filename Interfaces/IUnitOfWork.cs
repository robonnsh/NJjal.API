namespace Njal_back.Interfaces
{
    public interface IUnitOfWork
    {
        IproductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}
