using Microsoft.EntityFrameworkCore;
using StoreApi.Maps;

namespace StoreApi.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ProductMap(modelBuilder.Entity<Product>());
        }

    }
}