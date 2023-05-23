
namespace multiTenantApp.Models
{
    // sample business entity
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TenantId { get; set; }
    }
}
