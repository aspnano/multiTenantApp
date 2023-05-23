using Microsoft.EntityFrameworkCore;
using multiTenantApp.Models;
using System.Security.Claims;

namespace multiTenantApp.Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;

        }
        public async Task<bool> SetTenant(string tenant)
        {

            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync(); // check if tenant exists
            if (tenantInfo != null)
            {
                TenantId = tenant;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid"); 
            }

        }
        public string? TenantId { get; set; }
    }
}
