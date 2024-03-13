using Njal_back.Models;

namespace Njal_back.Interfaces
{
    public interface IproductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        void AddProduct(Product product);
        void DeleteProduct(Guid productId);
    }
}
