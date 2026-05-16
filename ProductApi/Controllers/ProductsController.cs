using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.DTOs;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Contollers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound(new {message = $"ID {id} olan ürün bulunamadı"});
            }
           
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
        {
            var product = await _productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new {id = product.Id},product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var success = await _productService.UpdateAsync(id, request);

            if(!success)
            {
                return NotFound(new {message = $"ID {id} olan ürün bulunamadı"});
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteAsync(id);
             if(!success)
            {
                return NotFound(new {message = $"ID {id} olan ürün bulunamadı"});
            }
            return NoContent();
        }
    }
}