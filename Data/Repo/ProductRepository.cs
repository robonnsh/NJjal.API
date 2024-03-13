using Microsoft.EntityFrameworkCore;
using Njal_back.Interfaces;
using Njal_back.Models;

namespace Njal_back.Data.Repo
{
    public class ProductRepository : IproductRepository
    {
        private readonly NjalDbContext dc;

        public ProductRepository(NjalDbContext dc)
        {
            this.dc = dc;
        }
        public void AddProduct(Product product)
        {
            dc.Products.Add(product);
        }

        public void DeleteProduct(Guid productId)
        {
            var product = dc.Products.Find(productId);
            dc.Products.Remove(product);
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await dc.Products.ToListAsync();
        }

       
    }
}
