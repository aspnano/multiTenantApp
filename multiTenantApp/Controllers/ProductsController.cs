using Microsoft.AspNetCore.Mvc;
using multiTenantApp.Services.ProductService;
using multiTenantApp.Services.ProductService.DTOs;

namespace queryFilterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService; 

        public ProductsController(IProductService productService)
        {
            _productService = productService; // inject the products service
        }
   
        // Get list of products
        [HttpGet]
        public IActionResult Get()
        {   
            var list = _productService.GetAllProducts(); 
            return Ok(list);
        }

        // Create a new product
        [HttpPost]
        public IActionResult Post(CreateProductRequest request)
        {
            var result = _productService.CreateProduct(request);
            return Ok(result);
        }

        // Delete a product by id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            return Ok(result);
        }

    }
}
