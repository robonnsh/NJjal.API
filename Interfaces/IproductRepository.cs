using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Njal_back.DTOS;
using Njal_back.Models;

namespace Njal_back.Interfaces
{
    public interface IproductRepository
    {
        // Get
        Task<IEnumerable<Product>> GetProductsAsync();
        // Post
        void AddProduct(Product product);
        //Delete
        void DeleteProduct(int productId);
        // Find Product
        //Task<Product> UpdateProduct(Guid productId, Product updateProductRequest);
        Task<Product> UpdateProduct(int productId, ProductDto updateProductRequest);

        // Update designer name
        Task<Product> UpdateDesignerName(int productId, DesignerNameDto updateDesignerNameReq);
        
        // Update product name 
        Task<Product> UpdateProductName(int productId, ProductNameDto updateProductNameReq);


        // Update price
        Task<Product> UpdatePrice(int productId, PriceDto updatePriceReq);

        // Find Patch currently not using 
        //Task<Product> FindPatchProduct(Guid id, JsonPatchDocument<Product> updateProductRequest);
    }
}
