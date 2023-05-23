using Microsoft.EntityFrameworkCore;
using multiTenantApp.Services;

namespace multiTenantApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId { get; set; }


        // Constructor -- convention used by Entity Framework 
        public ApplicationDbContext(ICurrentTenantService currentTenantService, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;

        }

        // DbSets -- create for all entity types to be managed with EF
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == CurrentTenantId); // multitenancy query filter for Application User

        }

        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries<Product>().ToList()) // Write tenant Id to table
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId; 
                        break;
                }
            }


            var result = base.SaveChanges();
            return result;
        }


    }
}
