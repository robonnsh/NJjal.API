using Microsoft.EntityFrameworkCore;
using Njal_back.Interfaces;
using Njal_back.Models;

namespace Njal_back.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly NjalDbContext dc;

        public UserRepository(NjalDbContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await dc.Users.FirstOrDefaultAsync
                (x => x.Username == userName && x.Password == password);

        }
    }
}
