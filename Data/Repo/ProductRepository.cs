using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Njal_back.DTOS;
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

        // Post
        public void AddProduct(Product product)
        {
            dc.Products.Add(product);
        }

        // Delete
        public void DeleteProduct(int productId)
        {
            var product = dc.Products.Find(productId);
            dc.Products.Remove(product);
        }

        // Get
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await dc.Products.ToListAsync();
        }

        // Put - update whole product
        public async Task<Product> UpdateProduct(int productId, ProductDto updateProductRequest)
        {
            var product = await dc.Products.FindAsync(productId);

            product.DesignerName = updateProductRequest.DesignerName;
            product.ProductName = updateProductRequest.ProductName;
            product.Price = updateProductRequest.Price;

            await dc.SaveChangesAsync();

            return product;
        }

        // Put - update designer name
        public async Task<Product> UpdateDesignerName(int productId, DesignerNameDto updateDesignerNameReq)
        {
            var designerName = await dc.Products.FindAsync(productId);
            designerName.DesignerName = updateDesignerNameReq.DesignerName;
            await dc.SaveChangesAsync();
            return designerName;
        }

        // Put - update product name 
        public async Task<Product> UpdateProductName(int productId, ProductNameDto updateProductNameReq)
        {
            var productName = await dc.Products.FindAsync(productId);
            productName.ProductName = updateProductNameReq.ProductName;
            await dc.SaveChangesAsync();
            return productName;
        }
        public async Task<Product> UpdatePrice(int productId, PriceDto updatePriceReq)
        {
            var price = await dc.Products.FindAsync(productId);
            price.Price = updatePriceReq.Price;
            await dc.SaveChangesAsync();
            return price;
        }



        // Patch currently not using 

        //public async Task<Product> FindPatchProduct(Guid productId, JsonPatchDocument<Product> updateProductRequest)
        //{
        //    var originalProduct = await dc.Products.FindAsync(productId);
        //    updateProductRequest.ApplyTo(originalProduct);
        //    return originalProduct;
        //}
    }
}
