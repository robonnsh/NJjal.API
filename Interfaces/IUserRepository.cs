using Njal_back.Models;

namespace Njal_back.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);  

        void Register(string userName, string password);

        Task<bool> UserAlreadyExist(string userName);
    }
}
