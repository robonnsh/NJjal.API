using Microsoft.AspNetCore.Mvc;
using Njal_back.Interfaces;
using Njal_back.Models;

namespace Njal_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ProductController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await uow.ProductRepository.GetProductsAsync();
            return Ok(products);
        }

        // Post api/product
        [HttpPost("add")]
        public async Task<IActionResult> AddProducts([FromBody] Product product)
        {
            uow.ProductRepository.AddProduct(product);
            await uow.SaveAsync();
            return StatusCode(201);
        }



        // Delete
        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            uow.ProductRepository.DeleteProduct(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
