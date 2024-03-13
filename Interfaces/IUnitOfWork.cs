namespace Njal_back.Interfaces
{
    public interface IUnitOfWork
    {
        IproductRepository ProductRepository { get; }
        Task<bool> SaveAsync();
    }
}
