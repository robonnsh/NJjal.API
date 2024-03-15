using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Njal_back.DTOS;
using Njal_back.Interfaces;
using Njal_back.Models;

namespace Njal_back.Controllers
{
    // add authorization if needed
    //[Authorize]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }


        // GET 
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await uow.ProductRepository.GetProductsAsync();
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        // Post 
        [HttpPost("add")]
        public async Task<IActionResult> AddProducts( ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            uow.ProductRepository.AddProduct(product);
            await uow.SaveAsync();
            return Ok(product);
        }

        // update whole product - Put  
        //[HttpPut]
        //[Route("update/{id:Guid}")]
        //public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Product Product)
        //{  
        //    var productFromDb = await uow.ProductRepository.UpdateProduct(id, Product);
        //    mapper.Map(ProductDto, productFromDb);
        //    await uow.SaveAsync();
        //    return Ok(productFromDb);
        //}

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductDto updateProductDto)
        {
            var productFromDb = await uow.ProductRepository.UpdateProduct(id, updateProductDto);
            mapper.Map(updateProductDto, productFromDb);
            await uow.SaveAsync();
            return Ok(productFromDb);
        }

        // update designer name only

        [HttpPut]
        [Route("updateDesignerName/{id}")]
        public async Task<IActionResult> UpdateDesignerName([FromRoute] int id, [FromBody] DesignerNameDto updateDesignerNameDto)
        {
            var designerNameFromDb = await uow.ProductRepository.UpdateDesignerName(id, updateDesignerNameDto);
            mapper.Map(updateDesignerNameDto, designerNameFromDb);
            await uow.SaveAsync();
            return Ok(designerNameFromDb);
        }

        // update product name only

        [HttpPut]
        [Route("updateProductName/{id}")]
        public async Task<IActionResult> UpdateProductName([FromRoute] int id, [FromBody] ProductNameDto updateProductNameDto)
        {
            var productNameFromDb = await uow.ProductRepository.UpdateProductName(id, updateProductNameDto);
            mapper.Map(updateProductNameDto, productNameFromDb);
            await uow.SaveAsync();
            return Ok(productNameFromDb);
        }

        // Update price only

        [HttpPut]
        [Route("updatePrice/{id}")]
        public async Task<IActionResult> UpdatePrice([FromRoute] int id, [FromBody] PriceDto updatePriceDto)
        {
            var priceFromDb = await uow.ProductRepository.UpdatePrice(id, updatePriceDto);
            mapper.Map(updatePriceDto, priceFromDb);
            await uow.SaveAsync();
            return Ok(priceFromDb);
        }


        // update - patch currently not using
        // 
        //[HttpPatch]
        //[Route("update/{id:Guid}")]
        //public async Task<IActionResult> UpdateProductsPatch([FromRoute] Guid id, [FromBody] JsonPatchDocument<Product> updateProductRequest)
        //{
        //    var productFromDb = await uow.ProductRepository.FindPatchProduct(id, updateProductRequest);
        //    await uow.SaveAsync();
        //    return Ok(productFromDb);
        //}



        // Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            uow.ProductRepository.DeleteProduct(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
