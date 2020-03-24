using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApi.Models;

namespace StoreApi.Maps
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("product");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Title).HasColumnName("title");
            entityBuilder.Property(x => x.Description).HasColumnName("description");
            entityBuilder.Property(x => x.Price).HasColumnName("price");

        }
    }
}