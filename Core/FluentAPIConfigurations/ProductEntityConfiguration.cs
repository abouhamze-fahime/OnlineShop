
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.FluentAPIConfigurations;
public class ProductEntityConfiguration :IEntityTypeConfiguration<Product>
{


 public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products"); // Map to table
        builder.HasKey(e => e.Id); // Define primary key
        builder.Property(e => e.ProductName)
               .HasColumnName("Title")
               .HasColumnType("nvarchar(250)")
               .IsRequired()
               .HasMaxLength(500);
        builder.Property(e=>e.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");
    }


}