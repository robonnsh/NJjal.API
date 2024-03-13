using Njal_back.Data.Repo;
using Njal_back.Interfaces;

namespace Njal_back.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NjalDbContext dc;

        public UnitOfWork(NjalDbContext dc)
        {
            this.dc = dc;
        }
        public IproductRepository ProductRepository =>
          new ProductRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
