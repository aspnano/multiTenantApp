using multiTenantApp.Models;
using multiTenantApp.Services.ProductService.DTOs;

namespace multiTenantApp.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}
