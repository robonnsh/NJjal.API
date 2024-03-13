using Microsoft.EntityFrameworkCore;
using Njal_back.Models;

namespace Njal_back.Data
{
    public class NjalDbContext : DbContext
    {
        public NjalDbContext(DbContextOptions<NjalDbContext> options) : base(options) { }
        
        public DbSet<Product> Products { get; set; }
    }
}
